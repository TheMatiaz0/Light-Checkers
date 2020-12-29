using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KillSelector : MoveSelector
{
	public Piece PieceToKill { get; set; }

	public override MoveSelector[] ClickSelection ()
	{
		Move();
		Kill();
		return ChangeTurnOrKillAgain();
	}

	protected MoveSelector[] ChangeTurnOrKillAgain ()
	{
		if (Piece.GetPositionsWithEnemies(Piece.GetCombinedDirections(), PointPosition).Count() > 0)
		{
			TileSelector.Instance.DeactiveAnyHighlight();
			return TileSelector.Instance.SelectPieceAndMoveOrKill(Piece);
		}

		ChangeTurn();
		return null;
	}

	protected void Kill ()
	{
		GameManager.Instance.KillPiece(PieceToKill);
	}
}
