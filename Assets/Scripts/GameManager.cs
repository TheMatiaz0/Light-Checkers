﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cyberevolver.Unity;
using System.Text;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public enum Mode
	{
		Local,
		LocalAI,
		AIAI,
		Online
	}

	public static GameManager Instance { get; private set; }

	[SerializeField]
	private Camera cam = null;

	[SerializeField]
	private Board board = null;

	[SerializeField]
	private Pawn[] pawnPrefabs = null;
	public Pawn[] PawnPrefabs => pawnPrefabs;

	[SerializeField]
	private Queen[] queenPrefabs = null;
	public Queen[] QueenPrefabs => queenPrefabs;

	public static Player[] Players { get; set; } = null;

	public Player CurrentPlayer { get; private set; }

	/// <summary>
	/// Pieces you have to/can remove from the board.
	/// </summary>
	public Piece[] WarPieces { get; private set; }

	public const int Width = 8;
	public const int Height = 8;

	[SerializeField]
	private FreezeMenu gameOverFreezeMenu = null;

	private List<Vector2Int> piecesToChange = new List<Vector2Int>();

	/// <summary>
	/// Determines if you can fight backwards with an enemy.
	/// </summary>
	public static bool FightBackwards { get; set; } = true;

	/// <summary>
	/// Do you have to attack enemy piece?
	/// </summary>
	public static bool MustAttack { get; set; } = true;

	/// <summary>
	/// Do you have to attack a grid where more pieces are?
	/// </summary>
	public static bool AttackMore { get; set; } = true;

	/// <summary>
	/// Is every pawn is a king piece?
	/// </summary>
	public static bool EveryKing { get; set; } = false;

	public static TimeSpan MaxTime { get; set; } = new TimeSpan(0, 5, 0);


	public static bool HideWoodenPlatforms { get; set; } = false;

	public static uint NumberForField { get; set; } = 0;



	/// <summary>
	/// You can't move if all your pieces are none or blocked or if you can attack as a piece.
	/// </summary>
	public bool ShouldMove { get; private set; } = true;

	public static bool StartedPreviousGame { get; private set; } = false;


	public uint TurnNumber { get; private set; } = 0;

	private bool firstRotate = false;

	private CameraMovement camMovement = null;

	[SerializeField]
	private AudioClip[] musicClips = null;

	[SerializeField]
	private Text playerOneName = null;

	[SerializeField]
	private Text playerTwoName = null;


	protected void Awake()
	{
		Instance = this;
	}

	public static Player[] GetMostDefaultPlayers()
	{
		return new Player[2] { new Player(Team.Black, "PlayerUno"), new Player(Team.White, "PlayerDuo") };
	}

	protected void Start()
	{
		camMovement = cam.GetComponent<CameraMovement>();

		StartGame();
	}

	private void StartGame ()
	{
		if (Players == null)
		{
			Players = GetMostDefaultPlayers();
		}

		if (StartedPreviousGame)
		{
			foreach (Player ply in Players)
			{
				ply.ResetPlayer();
			}
		}
		playerOneName.text = Players[1].Nickname;
		playerTwoName.text = Players[0].Nickname;

		// NOT Selection of the first player, it will be changed anyway.
		CurrentPlayer = Players.First(x => x.CurrentTeam == Team.Black);
		/*
		 * Debug mode:
		SpawnSinglePiece(2, 1, Team.Black, pawnPrefabs);
		SpawnSinglePiece(1, 2, Team.White, pawnPrefabs);

		SpawnSinglePiece(2, 3, Team.Black, pawnPrefabs);

		SpawnSinglePiece(2, 5, Team.Black, pawnPrefabs);
		SpawnSinglePiece(4, 5, Team.Black, pawnPrefabs);
		SpawnSinglePiece(6, 5, Team.Black, pawnPrefabs);
		*/

		/*
		SpawnSinglePiece(5, 2, Team.White, pawnPrefabs);
		SpawnSinglePiece(6, 1, Team.Black, queenPrefabs);
		// SpawnSinglePiece(7, 0, Team.Black, pawnPrefabs);

		SpawnSinglePiece(3, 4, Team.White, pawnPrefabs);
		SpawnSinglePiece(2, 5, Team.White, pawnPrefabs);

		SpawnSinglePiece(5, 6, Team.White, pawnPrefabs);
		*/


		// Spawn all pieces
		IReadOnlyList<Piece> pieces = EveryKing == true ? (IReadOnlyList<Piece>)(queenPrefabs as IReadOnlyList<Queen>) : (IReadOnlyList<Piece>)(pawnPrefabs as IReadOnlyList<Pawn>);
		DefaultSpawn(Team.White, pieces);
		DefaultSpawn(Team.Black, pieces);

		TimerController.Instance.SetupCountdown(Players, MaxTime);
		StartedPreviousGame = true;
		// First change of turn, turn = 1 from now on
		ChangeTurn();
	}

	private void DefaultSpawn(Team team, IReadOnlyList<Piece> figures = null)
	{
		for (int i = ((int)team * (Height - 3)); i < (team == Team.Black ? 3 : 8); i++)
		{
			SpawnLine(i, team, figures ?? pawnPrefabs);
		}
	}

	/// <summary>
	/// Spawns pieces with pattern (in this case, missing odd number [chessboard])
	/// </summary>
	/// <param name="row"></param>
	/// <param name="team"></param>
	private void SpawnLine (int row, Team team, IReadOnlyList<Piece> figures)
	{
		for (int col = 0; col < Width; col++)
		{
			if ((col + row) % 2 == NumberForField)
			{
				SpawnSinglePiece(col, row, team, figures);
				// Players[(int)team].PlayerPieces.Add(newlyCreatedPiece);

			}
		}
	}

	public Piece SpawnSinglePiece (int col, int row, Team pieceTeam, IReadOnlyList<Piece> figures, bool shouldAdd = true)
	{
		Piece newlyPiece = board.AddPiece(figures[(int)pieceTeam], col, row);

		if (shouldAdd)
		{
			Players.First(x => x.CurrentTeam == pieceTeam).PlayerPieces.Add(newlyPiece);
		}

		newlyPiece.CurrentTeam = pieceTeam;
		return newlyPiece;
	}

	public void SelectPiece (Piece pieceToSelect)
	{
		board.SelectPiece(pieceToSelect);
	}

	public void DeselectPiece (Piece pieceToDeselect)
	{
		board.DeselectPiece(pieceToDeselect);
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
					return piece;
				}
			}
		}

		return null;
	}

	public bool PlayerCanMove (Player playerToCheck)
	{
		List<Vector2Int> listForKeys = new List<Vector2Int>();
		Player ply = Players.FirstOrDefault(player => player == playerToCheck);

		foreach (Piece playerPiece in ply.PlayerPieces)
		{
			Dictionary<Vector2Int, Piece> allDirections = playerPiece.GetAllCorrectDirections();

			foreach (Vector2Int moveDir in allDirections.Keys)
			{
				return true;
			}

		}
		return false;
	}

	public IEnumerable<Piece> CurrentPlayerCanAttack ()
	{
		List<Piece> listForPieces = new List<Piece>();

		foreach (Piece playerPiece in CurrentPlayer.PlayerPieces)
		{
			Dictionary<Vector2Int, Piece> allDirections = playerPiece.GetAllCorrectDirections();

			foreach (Piece pieceToAttack in allDirections.Values)
			{
				if (pieceToAttack != null)
				{
					listForPieces.Add(pieceToAttack);
				}
			}
		}

		return listForPieces;
	}

	public Piece GetAtLeastOneAttackingPiece ()
	{
		foreach (Piece playerPiece in CurrentPlayer.PlayerPieces)
		{
			Dictionary<Vector2Int, Piece> allDirections = playerPiece.GetAllCorrectDirections();

			foreach (Piece pieceToAttack in allDirections.Values)
			{
				if (pieceToAttack != null)
				{
					return playerPiece;
				}
			}
		}

		return null;
	}

	public void ChangeTurn ()
	{
		TurnNumber += 1;

		// Clean up after the previous player
		TileSelector.Instance.DeactiveAnyHighlight();

		List<Player> losers = new List<Player>();

		for (int i = 0; i < piecesToChange.Count; i++)
		{
			Piece piece;

			if ((piece = GetFriendlyPieceAtGrid(piecesToChange[i])) != null)
			{
				UpgradePiece(piece);
			}
		}

		piecesToChange = new List<Vector2Int>();

		// Checking win conditions:

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
			ShouldMove = false;
			return;
		}

		// Change current player, next player:
		CurrentPlayer = GetOtherPlayerFromCurrent();
		cam.backgroundColor = CurrentPlayer.CurrentTeam == Team.Black ? new Color(0.12f, 0.12f, 0.12f) : new Color(0.76f, 0.76f, 0.76f);

		Piece[] piecesYouCanAttack = CurrentPlayerCanAttack().ToArray();

		if (MustAttack)
		{
			// goes false if there is minimum one piece to attack
			ShouldMove = !(piecesYouCanAttack.Length > 0);
		}

		WarPieces = piecesYouCanAttack;

		if (CurrentPlayer is NotPlayer)
		{
			NotPlayer ai = CurrentPlayer as NotPlayer;

			StartCoroutine(ai.EnterAIBehaviour());
			StopCoroutine(ai.EnterAIBehaviour());
		}

		if (firstRotate == false)
		{
			if (!(CurrentPlayer is NotPlayer))
			{
				camMovement.AdjustCamera(CurrentPlayer.CurrentTeam);
			}

			firstRotate = true;
		}

		TimerController.Instance.ChangeCountdown(CurrentPlayer);

		if (CurrentPlayer.PlayerPieces.Count <= 2 || GetOtherPlayerFromCurrent().PlayerPieces.Count <= 2)
		{
			MusicManager.Instance.ChangeMusic(musicClips[3]);
			return;
		}

		if (CurrentPlayer.PlayerPieces.Count <= 7 || GetOtherPlayerFromCurrent().PlayerPieces.Count <= 7)
		{
			MusicManager.Instance.ChangeMusic(musicClips[2]);
			return;
		}

		if (CurrentPlayer.PlayerPieces.Count <= 9 || GetOtherPlayerFromCurrent().PlayerPieces.Count <= 9)
		{
			MusicManager.Instance.ChangeMusic(musicClips[1]);
			return;
		}
	}

	public void SetGameOver (Player[] loser)
	{
		GameObject instanted = gameOverFreezeMenu.EnableMenuWithPause(true);
		GameOverMenu menu = instanted.GetComponent<GameOverMenu>();
		TimerController.Instance.StopAllCoroutines();

		if (loser.Length > 1)
		{
			Player morePointsPlayer = loser.Aggregate((x, y) => x.Points > y.Points ? x : y);
			if (morePointsPlayer == GetOtherPlayer(morePointsPlayer))
			{
				menu.WinInformation.text = "Draw! Both players had the same number of points.";
				return;
			}
			menu.WinInformation.text = $"Draw! The more points had <b>{morePointsPlayer.Nickname}</b> player.";         
			return;
		}
		// Team team = GetOtherPlayer(loser[0]).CurrentTeam;
		// string k = team == Team.Black ? "#1F1F1F" : "#C2C2C2";
		menu.WinInformation.text = $"<b>{GetOtherPlayer(loser[0]).Nickname}</b> as {GetOtherPlayer(loser[0]).CurrentTeam} team wins!";
	}
	//31, 194
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

	/// <summary>
	/// Can be upgraded to better piece?
	/// </summary>
	/// <param name="gridPoint"></param>
	/// <param name="team"></param>
	/// <returns></returns>
	public bool IsOnUpgradeGrid (Vector2Int gridPoint, Team team)
	{
		return team == Team.Black ? gridPoint.y >= 7 : gridPoint.y <= 0;
	}

	public Player GetOtherPlayerFromCurrent ()
	{
		return Players.FirstOrDefault(item => item != CurrentPlayer);
	}

	/// <summary>
	/// Searchs for CurrentPlayer piece
	/// </summary>
	/// <param name="gridPoint"></param>
	/// <returns></returns>
	public Piece GetFriendlyPieceAtGrid (Vector2Int gridPoint)
	{
		if (IsOutsideOfBoard(gridPoint))
		{
			return null;
		}

		foreach (Piece piece in CurrentPlayer.PlayerPieces)
		{
			if (Geometry.GridFromPoint(piece.transform.position) == gridPoint)
			{
				return piece;
			}
		}

		return null;
	}

	/// <summary>
	/// Searchs for non CurrentPlayer piece at grid.
	/// </summary>
	/// <param name="gridPoint"></param>
	/// <returns></returns>
	public Piece GetEnemyPieceAtGrid (Vector2Int gridPoint)
	{
		foreach (Piece piece in GetOtherPlayerFromCurrent().PlayerPieces)
		{
			if (Geometry.GridFromPoint(piece.transform.position) == gridPoint)
			{
				return piece;
			}
		}

		return null;
	}

	public void MovePiece (Piece pieceToMove, Vector2Int newPosition)
	{
		board.MovePiece(pieceToMove, newPosition);

		if (IsOnUpgradeGrid(newPosition, CurrentPlayer.CurrentTeam) == true)
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

	public void UpgradePiece (Piece pieceToUpgrade)
	{
		CurrentPlayer.Points += 30;
		Vector2Int vect = Geometry.GridFromPoint(pieceToUpgrade.transform.position);
		Piece piece = board.AddPiece(queenPrefabs[(int)CurrentPlayer.CurrentTeam], vect.x, vect.y);
		piece.CurrentTeam = pieceToUpgrade.CurrentTeam;
		CurrentPlayer.PlayerPieces.Remove(pieceToUpgrade);
		CurrentPlayer.PlayerPieces.Add(piece);
		Destroy(pieceToUpgrade.gameObject);
	}
}
