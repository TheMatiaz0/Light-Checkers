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
		MoveSelector[] moves = TileSelector.Instance.SelectPieceAndMoveOrKill(Piece, false);
		if (moves.Length > 0)
		{
			TileSelector.Instance.DeactiveAnyHighlight();
			return moves;
		}

		ChangeTurn();
		return null;
	}

	protected void Kill ()
	{
		GameManager.Instance.KillPiece(PieceToKill);
		SoundManager.Instance.PlayKill();
	}
}
