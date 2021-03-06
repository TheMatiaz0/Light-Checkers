﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
	Black = 0,
	White = 1
}

public class Player
{
	public List<Piece> PlayerPieces { get; set; }
	public Team CurrentTeam { get; }
	public string Nickname { get; }
	public int Points { get; set; }

	public float Time;

	public Player(Team team, string nickname)
	{
		PlayerPieces = new List<Piece>();
		CurrentTeam = team;
		Nickname = nickname;
	}

	public void ResetPlayer ()
	{
		Points = 0;
		PlayerPieces = new List<Piece>();
	}
}
