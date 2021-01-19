using Cyberevolver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOptions : MonoBehaviour
{
	[SerializeField]
	private Toggle fightBackwards = null;

	[SerializeField]
	private Toggle walkBackwards = null;

	[SerializeField]
	private Toggle mustAttack = null;
	[SerializeField]
	private Toggle moreAttacks = null;


	private GameManager.Mode mode = GameManager.Mode.Local;

	[SerializeField]
	private Pair<Dropdown> teamSelection = null;

	[SerializeField]
	private Pair<InputField> allNickname = null;
	// [SerializeField]
	// private Pair<ToggleGroup> allToggleGroups = null;

	protected void OnEnable()
	{
		fightBackwards.isOn = GameManager.FightBackwards;
		mustAttack.isOn = GameManager.MustAttack;
		walkBackwards.isOn = GameManager.MoveBackwards;
		moreAttacks.isOn = GameManager.AttackMore;
	}

	public void MustAttackCheck(bool isTrue)
	{
		GameManager.MustAttack = isTrue;
	}

	public void FightBackwardsCheck(bool isTrue)
	{
		GameManager.FightBackwards = isTrue;
	}

	public void PlayBtn()
	{
		Team one = (Team)teamSelection.First.value;
		Team two = (Team)teamSelection.Second.value;


		if (one == two)
		{
			Debug.Log("The same team");
			return;
		}

		if (string.IsNullOrEmpty(allNickname.First.text) || string.IsNullOrEmpty(allNickname.Second.text))
		{
			Debug.Log("Null name");
			return;
		}

		// Randomize nickname position not team, team stays the same.
		// [0], [1] - dwóch graczy. Losuj od 0 do 2.

		switch (mode)
		{
			case GameManager.Mode.Local:
				GameManager.Players = new Player[2] { new Player(one, allNickname.First.text), new Player(two, allNickname.Second.text) };
				break;

			case GameManager.Mode.AIAI:
				GameManager.Players = new Player[2] { new RandomAI(one, allNickname.First.text), new RandomAI(two, allNickname.Second.text) };
				break;

			case GameManager.Mode.LocalAI:
				GameManager.Players = new Player[2] { new Player(one, allNickname.First.text), new RandomAI(two, allNickname.Second.text) };
				break;

			case GameManager.Mode.Online:
				Debug.Log("Oj nie nie byczq");
				return;
		}

		SceneManager.LoadScene("Main");
	}

	public void WalkBackwardsCheck(bool isTrue)
	{
		GameManager.MoveBackwards = isTrue;
	}

	public void SelectModeDropdown(int option)
	{
		mode = (GameManager.Mode)option;
	}

	public void MoreAttacksToggle(bool isTrue)
	{
		GameManager.AttackMore = isTrue;
	}
}
