using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
	[SerializeField]
	private Text winInformation = null;
	public Text WinInformation => winInformation;

	[SerializeField]
	private FreezeMenu gameOverFreezeMenu = null;

	[SerializeField]
	private IconHighlighter[] buttons = null;

	protected void OnEnable()
	{
		TileSelector.Instance.InputActive = false;
		// TileSelector.Instance.DeactiveAnyHighlight();
	}

	protected void OnDisable()
	{
		// TileSelector.Instance.InputActive = true;
		foreach (var item in buttons)
		{
			item.Enable(false);
		}
	}

	public void SeeBoardBtn()
	{
		TileSelector.Instance.DeactiveAnyHighlight();
		gameOverFreezeMenu.SetupKeyCode(KeyCode.Escape);

		foreach (var item in gameOverFreezeMenu.BlockOtherFreezes)
		{
			item.gameObject.SetActive(false);
		}
		gameOverFreezeMenu.EnableMenuWithPause(false);
	}

	public void RestartGameBtn ()
	{
		gameOverFreezeMenu.EnableMenuWithPause(false);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void MainMenuBtn ()
	{
		gameOverFreezeMenu.EnableMenuWithPause(false);
		SceneManager.LoadScene("Menu");
	}
}
