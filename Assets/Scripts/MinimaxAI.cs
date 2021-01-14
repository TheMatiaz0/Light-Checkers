using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class MinimaxAI : NotPlayer
{
	public MinimaxAI(Team team, string name) : base(team, name)
	{
	}

	/*
	private List<Piece> OtherPlayerPieces = new List<Piece>();
	private List<Piece> ThisPlayerPieces = new List<Piece>();



	private async Task ChoosePieceAndMove(Piece pi, Vector2Int position)
	{
		await Task.Delay(100);

		// get all possible moves by chosen piece
		MoveSelector[] selectors = TileSelector.Instance.SelectPieceAndMoveOrKill(pi);

		await Task.Delay(500);
		// move or fight randomly
		selectors = selectors.First(x => x.PointPosition == position).ClickSelection();

		await Task.Delay(100);
		// if you have kill series then make it to the end.
		if (selectors != null)
		{
			selectors = TileSelector.Instance.SelectPieceAndMoveOrKill(pi);

			await ChoosePieceAndMove(pi, selectors[UnityEngine.Random.Range(0, selectors.Length)].PointPosition);
		}
	}

	protected override IEnumerator DepthAIBehaviour(List<Piece> copiedPieces, List<Piece> copiedOtherPlayerPieces)
	{
		MakeDeepCopy(copiedPieces, ThisPlayerPieces);
		MakeDeepCopy(copiedOtherPlayerPieces, OtherPlayerPieces);

		Piece piece;
		if ((piece = GameManager.Instance.GetAtLeastOneAttackingPiece()) != null)
		{
			_ = ChoosePieceAndMove(piece, TileSelector.Instance.SelectPieceAndMoveOrKill(piece)[0].PointPosition);
			yield return null;
			// randomized piece...
			// piece = copiedPieces[Random.Range(0, copiedPieces.Count)];
		}

		(float, Vector2Int, int) results = Minimax(Vector2Int.zero, 3, true);
		//Vector2Int finalPos = Minimax(Vector2Int.zero, 3, true).Item2;

		DestroyAllCopies(ThisPlayerPieces);
		DestroyAllCopies(OtherPlayerPieces);

		// ThisPlayerPieces = new List<Piece>();

		// OtherPlayerPieces = new List<Piece>();

		_ = (ChoosePieceAndMove(GameManager.Instance.CurrentPlayer.PlayerPieces[results.Item3], results.Item2));
		

		// _ = ChoosePieceAndMove(copiedPieces[2], new Vector2Int(6, 4));

		yield return null;
	}

	private void DestroyAllCopies(List<Piece> saveList)
	{
		foreach (Piece item in saveList)
		{
			UnityEngine.Object.Destroy(item.gameObject);
		}

		saveList.RemoveAll(x => x is Piece);
	}

	private void MakeDeepCopy(List<Piece> copiedPieces, List<Piece> saveList)
	{
		foreach (Piece p in copiedPieces)
		{
			Vector2Int position = Geometry.GridFromPoint(p.transform.position);
			Piece[] figures = null;

			if (p is Queen)
			{
				figures = GameManager.Instance.QueenPrefabs;
			}

			else
			{
				figures = GameManager.Instance.PawnPrefabs;
			}

			saveList.Add(GameManager.Instance.SpawnSinglePiece(position.x, position.y, p.CurrentTeam, figures, false));
		}
	}

	private float Evaluation()
	{
		return (ThisPlayerPieces.Count - OtherPlayerPieces.Count) + 1.5f * (ThisPlayerPieces.OfType<Queen>().Count() - OtherPlayerPieces.OfType<Queen>().Count());
	}

	private (float, Vector2Int, int) Minimax(Vector2Int position, int depth, bool maximize_player)
	{
		// on the max down... 
		if (depth == 0)
		{
			return (Evaluation(), position, 0);
		}

		if (maximize_player)
		{
			float maxEval = float.NegativeInfinity;
			Vector2Int bestMove = Vector2Int.zero;
			int piece = 0;

			// get all children nodes
			foreach ((Vector2Int, int) move in GetAllMoves(ThisPlayerPieces, OtherPlayerPieces))
			{
				float evaluation = Minimax(move.Item1, depth - 1, false).Item1;
				maxEval = Mathf.Max(maxEval, evaluation);
				piece = move.Item2;

				if (maxEval == evaluation)
				{
					bestMove = move.Item1;
				}
			}

			return (maxEval, bestMove, piece);
		}

		else
		{
			float minEval = float.PositiveInfinity;
			Vector2Int bestMove = Vector2Int.zero;
			int piece = 0;

			// get all children nodes
			foreach ((Vector2Int, int) move in GetAllMoves(OtherPlayerPieces, ThisPlayerPieces))
			{
				float evaluation = Minimax(move.Item1, depth - 1, true).Item1;
				minEval = Mathf.Min(minEval, evaluation);
				piece = move.Item2;

				if (minEval == evaluation)
				{
					bestMove = move.Item1;
				}
			}

			return (minEval, bestMove, piece);
		}
	}

	private List<(Vector2Int, int)> GetAllMoves(List<Piece> piecesOfWhichPlayer, List<Piece> oppositePieces)
	{
		List<(Vector2Int, int)> moves = new List<(Vector2Int, int)>();
		int k = 0;
		
		foreach (Piece piece in piecesOfWhichPlayer)
		{
			foreach (var posAndPiece in piece.GetAllCorrectDirections())
			{
				Vector2Int pos = posAndPiece.Key;
				Piece pieceToKill = posAndPiece.Value;
				Vector2Int move = SimulateMove(oppositePieces, pos, piece, pieceToKill);
				moves.Add((move, k));
			}

			k += 1;
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
	*/
	protected override IEnumerator DepthAIBehaviour(List<Piece> copiedPieces, List<Piece> copiedOtherPlayerPieces)
	{
		throw new NotImplementedException();
	}
}