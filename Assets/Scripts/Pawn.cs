using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
	public override Vector2Int[] MovementDirections { get; protected set; } = new Vector2Int[] { new Vector2Int(-1, 1), new Vector2Int(1, 1) };

	public override Vector2Int[] OtherDirections { get; protected set; } = new Vector2Int[] { new Vector2Int(-1, -1), new Vector2Int(1, -1) };

	public override int PointsForKill { get; protected set; } = 10;
}
