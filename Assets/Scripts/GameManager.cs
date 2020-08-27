using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cyberevolver.Unity;
using System.Text;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	[SerializeField]
	private Board board = null;

	[SerializeField]
	private Pawn[] pawnPrefabs = null;

	[SerializeField]
	private Queen[] queenPrefabs = null;

	public Player[] Players { get; private set; } = new Player[2];
	
	public Player CurrentPlayer { get; private set; }

	public const int Width = 8;
	public const int Height = 8;

	[SerializeField]
	private FreezeMenu gameOverFreezeMenu = null;

	private List<Vector2Int> piecesToChange = new List<Vector2Int>();

	public bool FightBack { get; } = true;

	public bool ShouldMove { get; private set; } = true;


	protected void Awake()
	{
		Instance = this;
	}

	protected void Start()
	{
		Players = new Player[2] { new Player(Team.Black, "Tomasz"), new Player(Team.White, "Maciej") };
		CurrentPlayer = Players[Random.Range(0, 2)];
		PieceSpawn(Team.Black);
		// SpawnLine(6, Team.White);
		PieceSpawn(Team.White);
	}

	private void PieceSpawn(Team team)
	{
		for (int i = ((int)team * (Height - 3)); i < (team == Team.Black ? 3 : 8); i++)
		{
			SpawnLine(i, team);
		}
	}

	private void SpawnLine (int row, Team team)
	{
		for (int col = 0; col < Width; col++)
		{
			if ((col + row) % 2 == 1)
			{
				Piece newlyCreatedPiece = board.AddPiece(pawnPrefabs[(int)team].gameObject, col, row).GetComponent<Piece>();
				Players[(int)team].PlayerPieces.Add(newlyCreatedPiece);
				newlyCreatedPiece.CurrentTeam = team;
			}
		}
	}

	public void SelectPiece (Piece pieceToSelect)
	{
		board.SelectPiece(pieceToSelect.gameObject);
	}

	public void DeselectPiece (Piece pieceToDeselect)
	{
		board.DeselectPiece(pieceToDeselect.gameObject);
	}

	public bool IsCurrentPlayerPiece (Piece pieceToCheck)
	{
		return CurrentPlayer.PlayerPieces.Contains(pieceToCheck);
	}

	public Piece GetAnyPieceAtGrid (Vector2Int gridPoint)
	{
		foreach (Player player in Players)
		{
			foreach (Piece piece in player.PlayerPieces)
			{
				if (Geometry.GridFromPoint(piece.transform.position).Equals(gridPoint))
				{
					return piece.GetComponent<Piece>();
				}
			}
		}

		return null;
	}

	public bool PlayerCanMove (Player playerToCheck)
	{
		var listForKeys = new List<Vector2Int>();
		Player playered = Players.FirstOrDefault(player => player == playerToCheck);
		foreach (Piece item in playered.PlayerPieces)
		{
			var allDirections = item.GetAllCorrectDirections();

			foreach (var item2 in allDirections.Keys)
			{
				listForKeys.Add(item2);
			}

		}

		return listForKeys.Count > 0;
	}

	public bool CurrentPlayerCanAttack ()
	{
		var listForPieces = new List<Piece>();

		foreach (var item in CurrentPlayer.PlayerPieces)
		{
			var allDirections = item.GetAllCorrectDirections();

			foreach (var item2 in allDirections.Values)
			{
				if (item2 != null)
				{
					listForPieces.Add(item2);
				}
			}
		}

		return listForPieces.Count > 0;
	}

	public void ChangeTurn ()
	{
		TileSelector.Instance.DeactiveMoveHighlight();

		List<Player> losers = new List<Player>();


		foreach (Vector2Int p in piecesToChange)
		{
			Piece piece;
			if ((piece = GetFriendlyPieceAtGrid(p)) != null)
			{
				ReplacePiece(piece);
			}
		}

		piecesToChange = new List<Vector2Int>();

		if (!PlayerCanMove(CurrentPlayer))
		{
			losers.Add(CurrentPlayer);
		}

		if (!PlayerCanMove(GetOtherPlayerFromCurrent()))
		{
			losers.Add(GetOtherPlayerFromCurrent());
		}

		if (losers.Count > 0)
		{
			SetGameOver(losers.ToArray());
			return;
		}

		CurrentPlayer = GetOtherPlayerFromCurrent();
		ShouldMove = !CurrentPlayerCanAttack();
	}


	public void SetGameOver (Player[] loser)
	{
		GameObject instanted = gameOverFreezeMenu.EnableMenuWithPause(true);
		GameOverMenu menu = instanted.GetComponent<GameOverMenu>();
		if (loser.Length > 1)
		{
			menu.WinInformation.text = "Draw!";
			return;
		}

		menu.WinInformation.text = $"{GetOtherPlayer(loser[0]).CurrentTeam} wins!";
	}

	public Player IsTherePlayerWithNoPieces()
	{
		return Players.FirstOrDefault(player => player.PlayerPieces.Count <= 0);
	}

	private Player GetOtherPlayer (Player notOtherPlayer)
	{
		return Players.FirstOrDefault(player => player != notOtherPlayer);
	}

	public bool IsOutsideOfBoard (Vector2Int gridPoint)
	{
		if (gridPoint.x < 0 || gridPoint.x > (Width - 1) || gridPoint.y < 0 || gridPoint.y > (Height - 1))
		{
			return true;
		}

		return false;
	}

	public bool IsOnChangeGrid (Vector2Int gridPoint, Team team)
	{
		switch (team)
		{
			case Team.Black:
				return gridPoint.y >= 7;

			case Team.White:
				return gridPoint.y <= 0;
		}

		return false;
	}

	private Player GetOtherPlayerFromCurrent ()
	{
		return Players.FirstOrDefault(item => item != CurrentPlayer);
	}

	public Piece GetFriendlyPieceAtGrid (Vector2Int gridPoint)
	{
		if (IsOutsideOfBoard(gridPoint))
		{
			return null;
		}

		foreach (Piece piece in CurrentPlayer.PlayerPieces)
		{
			if (Geometry.GridFromPoint(piece.transform.position).Equals(gridPoint))
			{
				return piece.GetComponent<Piece>();
			}
		}

		return null;
	}

	public Piece GetEnemyPieceAtGrid (Vector2Int gridPoint)
	{
		foreach (Piece piece in GetOtherPlayerFromCurrent().PlayerPieces)
		{
			if (Geometry.GridFromPoint(piece.transform.position) == (gridPoint))
			{
				return piece.GetComponent<Piece>();
			}
		}

		return null;
	}

	public void Move (Piece pieceToMove, Vector2Int newPosition)
	{
		board.MovePiece(pieceToMove.gameObject, newPosition);

		if (IsOnChangeGrid(newPosition, CurrentPlayer.CurrentTeam) == true)
		{
			piecesToChange.Add(newPosition);
		}
	}

	public void KillPiece (Piece pieceToKill)
	{
		CurrentPlayer.Points += pieceToKill.PointsForKill;
		GetOtherPlayerFromCurrent().PlayerPieces.Remove(pieceToKill);
		Destroy(pieceToKill.gameObject);
	}

	public void ReplacePiece (Piece pieceToReplace)
	{
		CurrentPlayer.Points += 30;
		Vector2Int vect = Geometry.GridFromPoint(pieceToReplace.transform.position);
		Piece piece = board.AddPiece(queenPrefabs[(int)CurrentPlayer.CurrentTeam].gameObject, vect.x, vect.y).GetComponent<Piece>();
		piece.CurrentTeam = pieceToReplace.CurrentTeam;
		CurrentPlayer.PlayerPieces.Remove(pieceToReplace);
		CurrentPlayer.PlayerPieces.Add(piece);
		Destroy(pieceToReplace.gameObject);
	}

	
}
