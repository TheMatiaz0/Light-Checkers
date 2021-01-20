using Cyberevolver.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	[SerializeField]
	private FreezeMenu gameOverFreezeMenu = null;

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
}
