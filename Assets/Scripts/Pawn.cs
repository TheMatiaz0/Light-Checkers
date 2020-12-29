using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pawn : Piece
{
	protected override Vector2Int[] MovementDirections { get; set; } = new Vector2Int[] { new Vector2Int(-1, 1), new Vector2Int(1, 1) };

	protected override Vector2Int[] OtherDirections { get; set; } = new Vector2Int[] { new Vector2Int(-1, -1), new Vector2Int(1, -1) };

	public override int PointsForKill { get; protected set; } = 1;

	protected override void Awake()
	{
		base.Awake();

		if (!GameManager.MoveBackwards)
		{
			return;
		}

		MovementDirections = MovementDirections.Concat(OtherDirections).ToArray();
	}
}
