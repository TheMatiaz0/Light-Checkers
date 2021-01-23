using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Piece : MonoBehaviour
{
	public MeshRenderer MeshRenderer { get; private set; } = null;

	public Team CurrentTeam { get; set; }
	protected virtual Vector2Int[] MovementDirections { get; set; } = new Vector2Int[] { new Vector2Int(-1, 1), new Vector2Int(1, 1) };

	// For moving backwards, NOT WORKING AT THE MOMENT
	protected virtual Vector2Int[] OtherDirections { get; set; } = new Vector2Int[] { new Vector2Int(-1, -1), new Vector2Int(1, -1) };

	public virtual int PointsForKill { get { return _PointsForKill * (CurrentTeam == Team.Black ? -1 : 1); } protected set { _PointsForKill = value; } }
	private int _PointsForKill;


	private List<Vector2Int> trueDirections = new List<Vector2Int>();


	protected virtual void Awake()
	{
		MeshRenderer = GetComponent<MeshRenderer>();
		if (GameManager.FightBackwards == false)
		{
			OtherDirections = new Vector2Int[0];
		}
	}

	public virtual Dictionary<Vector2Int, Piece> GetAllCorrectDirections()
	{
		Dictionary<Vector2Int, Piece> locations = new Dictionary<Vector2Int, Piece>();

		LoopThroughMovementDirections(ref locations, MovementDirections);
		LoopThroughOtherDirections(ref locations, OtherDirections);
		if (GameManager.MustAttack)
		{
			RemoveMoveIfCanAttack(ref locations);
		}

		if (GameManager.AttackMore)
		{
			List<Vector2Int> allToSearch = locations.Where(x => x.Value != null).Select(item => item.Key).ToList();

			if (allToSearch.Count > 1)
			{
				RemoveWorseAttacks(ref locations, allToSearch);
			}
		}

		return locations;
	}

	protected void RemoveWorseAttacks(ref Dictionary<Vector2Int, Piece> locations, List<Vector2Int> attackAble)
	{
		Dictionary<Vector2Int, float> greatAttackPoints = new Dictionary<Vector2Int, float>();
		Vector2Int[] afterMovePosition;

		for (int i = 0; i < attackAble.Count; i++)
		{
			// get position of enemies at new position.
			afterMovePosition = GetPositionsWithEnemies(GetCombinedDirections(), attackAble[i]).ToArray();

			if (afterMovePosition.Length <= 0)
			{
				greatAttackPoints.Add(attackAble[i], 1);
			}

			else
			{
				if (!greatAttackPoints.ContainsKey(attackAble[i]))
				{
					greatAttackPoints.Add(attackAble[i], afterMovePosition.Length);
					attackAble.Add(attackAble[i]);
				}
			}
		}

		Vector2Int finalPosition = greatAttackPoints.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
		locations.RemoveAll((key, value) => key != finalPosition);
	}

	protected void RemoveMoveIfCanAttack(ref Dictionary<Vector2Int, Piece> locations)
	{
		RemoveMoveIfCanAttack(locations);
	}

	private void RemoveMoveIfCanAttack(Dictionary<Vector2Int, Piece> locations)
	{
		if (locations.Values.Any(value => value != null))
		{
			locations.RemoveAll((key, value) => value == null);
		}
	}

	protected Dictionary<Vector2Int, Piece> LoopThroughMovementDirections(ref Dictionary<Vector2Int, Piece> locations, Vector2Int[] movementDirections, bool? isOther = false)
	{
		trueDirections = movementDirections.ToList();

		for (int i = 0; i < trueDirections.Count; i++)
		{
			Vector2Int newDir = (trueDirections[i] + Geometry.GridFromPoint(this.transform.position));
			newDir = ReturnDirectionNormalizedByTeam(newDir, isOther);

			// check if location already exists...
			if (locations.Keys.Contains(newDir))
			{
				continue;
			}

			// Queen problem resolved :D
			if (GameManager.Instance.GetFriendlyPieceAtGrid(newDir) != null)
			{
				Vector2Int diag = trueDirections[i];

				for (int eachStep = 1; eachStep <= 7; eachStep++)
				{
					diag += GetDiagnonalToAdd(diag.x * eachStep, diag.y * eachStep);

					trueDirections.Remove(diag);
				}

				continue;
			}

			// Find out if enemy is null on this grid and if it isn't then add it to the locations:
			if (FindEnemy(ref locations, newDir, trueDirections[i], isOther) != null)
			{
				continue;
			}

			// Finally, without enemies, check if movement grid is good (without errors):
			if (CheckIsGridGood(newDir))
			{
				locations.Add(newDir, null);
			}
		}

		return locations;

	}

	protected Dictionary<Vector2Int, Piece> LoopThroughOtherDirections(ref Dictionary<Vector2Int, Piece> locations, Vector2Int[] directionsToLoop)
	{
		foreach (Vector2Int dir in directionsToLoop)
		{
			Vector2Int newDir = (dir + Geometry.GridFromPoint(this.transform.position));
			newDir = ReturnDirectionNormalizedByTeam(newDir, true);

			FindEnemy(ref locations, newDir, dir, true);

			continue;
		}

		return locations;
	}

	public IEnumerable<Vector2Int> GetPositionsWithEnemies(Vector2Int[] directionsToCheck, Vector2Int yourPosition)
	{
		List<Vector2Int> positions = new List<Vector2Int>();

		foreach (Vector2Int dir in directionsToCheck)
		{
			Vector2Int newDir = dir + yourPosition;

			if (FindEnemy(newDir, dir) != null)
			{
				positions.Add(newDir);
				break;
			}
		}

		return positions;
	}

	/// <summary>
	/// Directions combined with backwards and normal movement of piece.
	/// </summary>
	/// <returns></returns>
	public Vector2Int[] GetCombinedDirections()
	{
		return MovementDirections.Concat(OtherDirections).ToArray();
	}

	protected Vector2Int ReturnDirectionNormalizedByTeam(Vector2Int newDir, bool? isOther = null)
	{
		if (!isOther.HasValue)
		{
			return newDir;
		}

		if (isOther.Value)
		{
			return new Vector2Int(newDir.x, newDir.y + (int)CurrentTeam * 2);
		}

		else
		{
			return new Vector2Int(newDir.x, newDir.y - (int)CurrentTeam * 2);
		}
	}

	protected Piece FindEnemy(Vector2Int newDir, Vector2Int dir)
	{
		Piece piece;

		if ((piece = GameManager.Instance.GetEnemyPieceAtGrid(newDir)) != null)
		{
			// adding for free space
			newDir += dir;

			if (CheckIsGridGood(newDir))
			{
				return piece;
			}
		}

		return null;
	}


	protected Piece FindEnemy(ref Dictionary<Vector2Int, Piece> locations, Vector2Int newDir, Vector2Int moveDir, bool? isOther = null)
	{
		Piece piece;

		if ((piece = GameManager.Instance.GetEnemyPieceAtGrid(newDir)) != null)
		{
			Vector2Int diagonal = GetDiagnonalToAdd(moveDir.x, moveDir.y);
			// direction on diagnonal of current enemy (place to attack):
			newDir += diagonal;

			newDir = ReturnDirectionNormalizedByTeam(newDir, isOther);


			for (int i = 2; i <= 7; i++)
			{
				moveDir += GetDiagnonalToAdd(moveDir.x * i, moveDir.y * i);
				trueDirections.Remove(moveDir);
			}

			// Check if you can go diagonal grid and beat the enemy:
			if (CheckIsGridGood(newDir))
			{
				// remove bad locations here
				locations.Add(newDir, piece);
			}


		}

		return null;
	}

	private Vector2Int GetDiagnonalToAdd(int x, int y)
	{
		return new Vector2Int(x / Math.Abs(x), y / Math.Abs(y));
	}


	protected bool CheckIsGridGood(Vector2Int newDir)
	{
		if (GameManager.Instance.GetAnyPieceAtGrid(newDir) == null && GameManager.Instance.IsOutsideOfBoard(newDir) == false)
		{
			return true;
		}

		return false;
	}
}
