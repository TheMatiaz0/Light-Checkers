using Cyberevolver;
using System;
using System.Collections;
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
	private Pair<Toggle> darkSelection = null;
	[SerializeField]
	private Pair<Toggle> lightSelection = null;

	[SerializeField]
	private Pair<InputField> allNickname = null;

	[SerializeField]
	private GameObject[] rules = null;

	[SerializeField]
	private GameObject[] playersSection = null;

	[SerializeField]
	private GameObject[] eachPlayer = null;

	[SerializeField]
	private InputField timeLimit = null;

	protected void OnEnable()
	{
		Loop();

		StartCoroutine(LoopArrayWithDelay(rules, 0.09f, 
			LoopArrayWithDelay(playersSection, 0.12f, LoopArrayWithDelay(eachPlayer, 0.06f, null))));

		fightBackwards.isOn = GameManager.FightBackwards;
		mustAttack.isOn = GameManager.MustAttack;
		walkBackwards.isOn = GameManager.MoveBackwards;
		moreAttacks.isOn = GameManager.AttackMore;
	}

	private IEnumerator LoopArrayWithDelay(GameObject[] arr, float seconds, IEnumerator next)
	{
		foreach (var item in arr)
		{
			item.SetActive(true);
			yield return new WaitForSeconds(seconds);
		}

		yield return next;
	}

	public void MustAttackCheck(bool isTrue)
	{
		GameManager.MustAttack = isTrue;
	}

	public void FightBackwardsCheck(bool isTrue)
	{
		GameManager.FightBackwards = isTrue;
	}

	private void AppearDisappear(RectTransform rectTransform)
	{
		LeanTween.cancel(rectTransform);

		LeanTween.alphaText(rectTransform, 1, 0.05f)
	    .setOnComplete(() => LeanTween.alphaText(rectTransform, 0, 6f));
	}

	public void PlayBtn()
	{
		// DARK: one playerOne == true, two playerTwo == false
		// LIGHT: three playerOne == false, four playerTwo == true
		bool one = darkSelection.First.isOn;
		bool two = darkSelection.Second.isOn;
		bool three = lightSelection.First.isOn;
		bool four = lightSelection.Second.isOn;

		Team playerOneTeam = Team.White;
		Team playerTwoTeam = Team.Black;



		if (one == two)
		{
			AppearDisappear(ErrorInfoManager.Instance.PlayerTwoError.rectTransform);
			ErrorInfoManager.Instance.PlayerTwoError.text = "Are you nuts? You can't play in the same team. This is COMPETITIVE GAME.";
			return;
		}

		if (three == four)
		{
			AppearDisappear(ErrorInfoManager.Instance.PlayerOneError.rectTransform);
			ErrorInfoManager.Instance.PlayerOneError.text = "Are you nuts? You can't play in the same team. This is COMPETITIVE GAME.";
			return;
		}

		if (string.IsNullOrEmpty(allNickname.First.text))
		{
			AppearDisappear(ErrorInfoManager.Instance.PlayerOneError.rectTransform);
			ErrorInfoManager.Instance.PlayerOneError.text = "Invalid nickname, don't mess with input fields.";
			return;
		}

		if (string.IsNullOrEmpty(allNickname.Second.text))
		{
			AppearDisappear(ErrorInfoManager.Instance.PlayerTwoError.rectTransform);
			ErrorInfoManager.Instance.PlayerTwoError.text = "Invalid nickname, don't mess with input fields.";
			return;
		}

		if (one)
		{
			playerOneTeam = Team.Black;
		}

		if (two)
		{
			playerTwoTeam = Team.Black;
		}

		if (three)
		{
			playerOneTeam = Team.White;
		}

		if (four)
		{
			playerTwoTeam = Team.White;
		}

		switch (mode)
		{
			case GameManager.Mode.Local:
				GameManager.Players = new Player[2] { new Player(playerOneTeam, allNickname.First.text), new Player(playerTwoTeam, allNickname.Second.text) };
				GameManager.MaxTime = new TimeSpan(0, int.Parse(timeLimit.text), 0);
				break;

			case GameManager.Mode.AIAI:
				GameManager.Players = new Player[2] { new RandomAI(playerOneTeam, allNickname.First.text), new RandomAI(playerTwoTeam, allNickname.Second.text) };
				GameManager.MaxTime = new TimeSpan(0, int.Parse(timeLimit.text), 0);
				break;

			case GameManager.Mode.LocalAI:
				GameManager.Players = new Player[2] { new Player(playerOneTeam, allNickname.First.text), new RandomAI(playerTwoTeam, allNickname.Second.text) };
				GameManager.MaxTime = new TimeSpan(0, int.Parse(timeLimit.text), 0);
				break;

			case GameManager.Mode.Online:
				AppearDisappear(ErrorInfoManager.Instance.DropdownError.rectTransform);
				ErrorInfoManager.Instance.DropdownError.text = "Maybe one day...";
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

	private void Loop()
	{
		foreach (var item in rules)
		{
			item.SetActive(false);
		}

		foreach (var item in playersSection)
		{
			item.SetActive(false);
		}

		foreach (var item in eachPlayer)
		{
			item.SetActive(false);
		}
	}

	public void ClickReturnBtn()
	{
		StopAllCoroutines();

		Loop();

		MainMenu.Instance.BtnAnimation(0, true);
	}
}
