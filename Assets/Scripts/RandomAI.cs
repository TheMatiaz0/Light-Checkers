using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAI : NotPlayer
{
	public RandomAI(Team team, string name) : base(team, name)
	{
	}

	protected override IEnumerator DepthAIBehaviour(List<Piece> copiedPieces, List<Piece> copiedOtherPlayerPieces)
	{
		Piece piece;
		if ((piece = GameManager.Instance.GetAtLeastOneAttackingPiece()) == null)
		{
			// randomized piece...
			piece = copiedPieces[Random.Range(0, copiedPieces.Count)];
		}

		// Can you move with currently selected piece?
		if (piece.GetAllCorrectDirections().Count > 0)
		{
			yield return ChoosePieceAndMove(piece);
		}

		else
		{
			copiedPieces.Remove(piece);
			yield return DepthAIBehaviour(copiedPieces, copiedOtherPlayerPieces);
		}
	}

	private IEnumerator ChoosePieceAndMove(Piece pi)
	{
		yield return new WaitForSeconds(0.1f);
		// get all possible moves by chosen piece
		MoveSelector[] selectors = TileSelector.Instance.SelectPieceAndMoveOrKill(pi);

		yield return new WaitForSeconds(0.5f);
		// move or fight randomly
		selectors = selectors[Random.Range(0, selectors.Length)].ClickSelection();
		yield return new WaitForSeconds(0.1f);
		// if you have kill series then make it to the end.
		if (selectors != null)
		{
			yield return ChoosePieceAndMove(pi);
		}
	}
}
