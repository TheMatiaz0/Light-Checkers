using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/*
public class MinimaxAI : NotPlayer
{
	private List<Piece> OtherPlayerPieces;
	private List<Piece> ThisPlayerPieces;

	public MinimaxAI(Team team, string name) : base(team, name)
	{
	}

	protected override IEnumerator DepthAIBehaviour(List<Piece> copiedPieces, List<Piece> copiedOtherPlayerPieces)
	{
		ThisPlayerPieces = copiedPieces;
		OtherPlayerPieces = copiedOtherPlayerPieces;
		Vector2Int finalPos = Minimax(Vector2Int.zero, 3, true).Item2;
		Debug.Log(finalPos);
		yield return null;
	}
	private float Evaluation()
	{
		return (ThisPlayerPieces.Count - OtherPlayerPieces.Count) + 1.5f * (ThisPlayerPieces.OfType<Queen>().Count() - OtherPlayerPieces.OfType<Queen>().Count());
	}

	private (float, Vector2Int) Minimax(Vector2Int position, int depth, bool maximize_player)
	{
		// on the max down... 
		if (depth == 0)
		{
			return (Evaluation(), position);
		}

		if (maximize_player)
		{
			float maxEval = float.NegativeInfinity;
			Vector2Int bestMove = Vector2Int.zero;

			// get all children nodes
			foreach (Vector2Int move in GetAllMoves(ThisPlayerPieces, OtherPlayerPieces))
			{
				float evaluation = Minimax(move, depth - 1, false).Item1;
				maxEval = Mathf.Max(maxEval, evaluation);
				if (maxEval == evaluation)
				{
					bestMove = move;
				}
			}

			return (maxEval, bestMove);
		}

		else
		{
			float minEval = float.PositiveInfinity;
			Vector2Int bestMove = Vector2Int.zero;

			// get all children nodes
			foreach (Vector2Int move in GetAllMoves(OtherPlayerPieces, ThisPlayerPieces))
			{
				float evaluation = Minimax(move, depth - 1, true).Item1;
				minEval = Mathf.Min(minEval, evaluation);
				if (minEval == evaluation)
				{
					bestMove = move;
				}
			}

			return (minEval, bestMove);
		}
	}

	private List<Vector2Int> GetAllMoves(List<Piece> piecesOfWhichPlayer, List<Piece> oppositePieces)
	{
		List<Vector2Int> moves = new List<Vector2Int>();
		
		foreach (Piece piece in piecesOfWhichPlayer)
		{
			foreach (var posAndPiece in piece.GetAllCorrectDirections())
			{
				Vector2Int pos = posAndPiece.Key;
				Piece pieceToKill = posAndPiece.Value;
				var wtf = SimulateMove(oppositePieces, pos, piece, pieceToKill);
				moves.Add(wtf);
			}
		}

		return moves;
	}

	private Vector2Int SimulateMove (List<Piece> oppositePieces, Vector2Int posToCheck, Piece forPiece, Piece pieceToKill)
	{
		forPiece.transform.position = Geometry.PointFromGrid(posToCheck);

		if (pieceToKill != null)
		{
			oppositePieces.Remove(pieceToKill);
		}

		return Geometry.GridFromPoint(forPiece.transform.position);
	}
}
*/
