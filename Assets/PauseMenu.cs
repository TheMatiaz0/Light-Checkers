using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	[SerializeField]
	private FreezeMenu freezeMenu = null;

	[SerializeField]
	private IconHighlighter[] buttons = null;

	public void ClickResumeBtn()
	{
		freezeMenu.EnableMenuWithPause(false);
	}

	public void RestartGameBtn()
	{
		freezeMenu.EnableMenuWithPause(false);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void ClickMakeDrawBtn()
	{
		freezeMenu.EnableMenuWithPause(false);
		GameManager.Instance.SetGameOver(GameManager.Players);
	}

	public void MainMenuBtn()
	{
		freezeMenu.EnableMenuWithPause(false);
		SceneManager.LoadScene("Menu");
	}

	protected void OnEnable()
	{
		foreach (var item in buttons)
		{
			item.Enable(true);
		}

		TileSelector.Instance.InputActive = false;
		TileSelector.Instance.DeactiveAnyHighlight();
	}

	protected void OnDisable()
	{
		TileSelector.Instance.InputActive = true;
		foreach (var item in buttons)
		{
			item.Enable(false);
		}
	}
}
