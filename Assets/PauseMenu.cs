using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	[SerializeField]
	private FreezeMenu gameOverFreezeMenu = null;

	[SerializeField]
	private IconHighlighter[] buttons = null;

	public void RestartGameBtn()
	{
		gameOverFreezeMenu.EnableMenuWithPause(false);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void MainMenuBtn()
	{
		gameOverFreezeMenu.EnableMenuWithPause(false);
		SceneManager.LoadScene("Menu");
	}

	protected void OnEnable()
	{
		TileSelector.Instance.InputActive = false;
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
