using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Piece : MonoBehaviour
{
	public MeshRenderer MeshRenderer { get; private set; } = null;

	public Team CurrentTeam { get; set; }
	public virtual Vector2Int[] MovementDirections { get; protected set; } = new Vector2Int[] { new Vector2Int(-1, 1), new Vector2Int(1, 1) };
	public virtual Vector2Int[] OtherDirections { get; protected set; } = new Vector2Int[] { new Vector2Int(-1, -1), new Vector2Int(1, -1) };

	public virtual int PointsForKill { get; protected set; } = 10;

	protected virtual void Awake()
	{
		MeshRenderer = GetComponent<MeshRenderer>();
		if (GameManager.Instance.FightBack == false)
		{
			OtherDirections = new Vector2Int[0];
		}
	}

	public virtual Dictionary<Vector2Int, Piece> GetAllCorrectDirections()
	{
		Dictionary<Vector2Int, Piece> locations = new Dictionary<Vector2Int, Piece>();

		LoopThroughMovementDirections(ref locations, MovementDirections);
		LoopThroughOtherDirections(ref locations, OtherDirections);

		RemoveMoveIfAttack(ref locations);

		return locations;
	}

	protected void RemoveMoveIfAttack(ref Dictionary<Vector2Int, Piece> locations)
	{
		if (locations.Values.Any(value => value != null))
		{
			locations.RemoveAll((key, value) => value == null);
		}
	}

	protected Dictionary<Vector2Int, Piece> LoopThroughMovementDirections(ref Dictionary<Vector2Int, Piece> locations, Vector2Int[] movementDirections, bool? isOther = false)
	{
		foreach (Vector2Int dir in movementDirections)
		{
			Vector2Int newDir = (dir + Geometry.GridFromPoint(this.transform.position));
			newDir = ReturnDirectionNormalizedByTeam(newDir, isOther);

			if (locations.Keys.Contains(newDir))
			{
				continue;
			}

			if (FindEnemy(ref locations, newDir, dir, isOther) != null)
			{
				continue;
			}

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

	public List<Vector2Int> GetPositionsWithEnemies(Vector2Int[] directionsToCheck)
	{
		List<Vector2Int> positions = new List<Vector2Int>();

		foreach (Vector2Int dir in directionsToCheck)
		{
			Vector2Int newDir = (dir + Geometry.GridFromPoint(this.transform.position));

			if (FindEnemy(newDir, dir) != null)
			{
				positions.Add(newDir);
				break;
			}
		}

		return positions;
	}

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
			return new Vector2Int(newDir.x, (newDir.y + (int)CurrentTeam * 2));
		}

		else
		{
			return new Vector2Int(newDir.x, (newDir.y - (int)CurrentTeam * 2));
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

	protected Piece FindEnemy(ref Dictionary<Vector2Int, Piece> locations, Vector2Int newDir, Vector2Int dir, bool? isOther = null)
	{
		Piece piece;

		if ((piece = GameManager.Instance.GetEnemyPieceAtGrid(newDir)) != null)
		{
			// adding for free space
			newDir += new Vector2Int(dir.x / Math.Abs(dir.x), dir.y / Math.Abs(dir.y));

			newDir = ReturnDirectionNormalizedByTeam(newDir, isOther);

			if (CheckIsGridGood(newDir))
			{
				locations.Add(newDir, piece);
			}
		}

		return null;
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
