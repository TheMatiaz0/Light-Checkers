using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KillSelector : MoveSelector
{
	public Piece PieceToKill { get; set; }

	protected override void OnMouseDown()
	{
		Move();
		Kill();
		ChangeTurnOrKillAgain();
	}

	protected void ChangeTurnOrKillAgain ()
	{
		List<Vector2Int> positions;

		if ((positions = Piece.GetPositionsWithEnemies(Piece.GetCombinedDirections())) != null && positions.Count > 0)
		{
			foreach (Vector2Int pos in positions)
			{
				TileSelector.Instance.CreateKillSelect(Piece, pos, GameManager.Instance.GetEnemyPieceAtGrid(pos));
			}
		}

		else
		{
			ChangeTurn();
		}
	}

	protected void Kill ()
	{
		GameManager.Instance.KillPiece(PieceToKill);
	}
}
