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

	[SerializeField]
	private Text gameOverText = null;

	[SerializeField]
	private Text header = null;

	[SerializeField]
	private Transform[] decos = null;

	[SerializeField]
	private Image img = null;


	protected void OnEnable()
	{
		TileSelector.Instance.InputActive = false;

		foreach (var item in buttons)
		{
			item.gameObject.SetActive(false);
		}

		foreach (var item in decos)
		{
			item.localScale = new Vector2(0, item.localScale.y);
		}

		img.color = new Color32(41, 41, 41, 0);
		header.color = new Color32(255, 255, 255, 0);
		gameOverText.color = new Color32(255, 255, 255, 0);
		winInformation.color = new Color32(255, 255, 255, 0);
		//header.SetActive(false);
		// gameOverText.gameObjectSetActive(false);


		LeanTween.alpha(img.rectTransform, 1, 0.6f).setIgnoreTimeScale(true).setOnComplete(() => StartCoroutine(ExpandDecos()));
	}

	private void ShowOther()
	{
		// header.SetActive(true);
		// gameOverText.SetActive(true);

		// LeanTween.alpha(winInformation.rectTransform, 1, 0.6f).setIgnoreTimeScale(true).setOnComplete(() => StartCoroutine(ExpandDecos()));

		/*
		LeanTween.alpha(header.rectTransform, 1, 0.2f).setIgnoreTimeScale(true).setOnComplete(() => 
		LeanTween.alpha(gameOverText.rectTransform, 1, 0.1f).setIgnoreTimeScale(true).setOnComplete(() => 
		LeanTween.alpha(winInformation.rectTransform, 1, 0.2f).setIgnoreTimeScale(true).setOnComplete(() => StartCoroutine(ExpandDecos()))));
		*/

	}

	private IEnumerator ExpandDecos()
	{
		LeanTween.alphaText(header.rectTransform, 1, 0.2f).setIgnoreTimeScale(true).setOnComplete(() =>
		LeanTween.alphaText(gameOverText.rectTransform, 1, 0.1f).setIgnoreTimeScale(true).setOnComplete(() =>
		LeanTween.alphaText(winInformation.rectTransform, 1, 0.2f).setIgnoreTimeScale(true)));

		yield return new WaitForSecondsRealtime(0.6f);

		foreach (var item in decos)
		{
			LeanTween.scaleX(item.gameObject, 1, 2).setIgnoreTimeScale(true);

		}

		yield return new WaitForSecondsRealtime(0.1f);

		foreach (var item in buttons)
		{
			item.gameObject.SetActive(true);
			yield return new WaitForSecondsRealtime(0.7f);
		}

		foreach (var item in buttons)
		{
			item.Enable(true);
		}
	}

	protected void OnDisable()
	{
		foreach (var item in buttons)
		{
			item.Enable(false);
		}
	}

	public void SeeBoardBtn()
	{
		// TileSelector.Instance.DeactiveAnyHighlight();
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
