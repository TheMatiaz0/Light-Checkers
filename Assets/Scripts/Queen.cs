using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
	protected override Vector2Int[] MovementDirections { get; set; } = new Vector2Int[] { new Vector2Int(-1, 1), new Vector2Int(-1, -1), new Vector2Int(1, -1), new Vector2Int(1, 1) };

	protected override void Awake()
	{
		List<Vector2Int> movementDirections = new List<Vector2Int>();
		movementDirections.AddRange(MovementDirections);

		foreach (Vector2Int item in MovementDirections)
		{
			movementDirections.AddRange(CreateDirections(item));
		}

		MovementDirections = movementDirections.ToArray();
	}

	/// <summary>
	/// Queen can move everywhere on grid with diagonals.
	/// </summary>
	/// <param name="dir"></param>
	/// <returns></returns>
	private IEnumerable<Vector2Int> CreateDirections (Vector2Int dir)
	{
		for (int i = 2; i <= 7; i++)
		{
			yield return dir * i;
		}
	}

	protected override Vector2Int[] OtherDirections { get; set; } = new Vector2Int[0];

	public override int PointsForKill { get; protected set; } = 3;

	public override Dictionary<Vector2Int, Piece> GetAllCorrectDirections()
	{
		Dictionary<Vector2Int, Piece> positions = new Dictionary<Vector2Int, Piece>();

		LoopThroughMovementDirections(ref positions, MovementDirections, null);

		if (GameManager.MustAttack)
		{
			RemoveMoveIfCanAttack(ref positions);
		}


		return positions;
	}
}
