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

	public void RestartGameBtn ()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void MainMenuBtn ()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
