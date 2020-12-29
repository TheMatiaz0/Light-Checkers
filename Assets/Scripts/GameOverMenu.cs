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
