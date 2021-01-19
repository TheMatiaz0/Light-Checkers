using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using Cyberevolver;

public class MainMenu : MonoBehaviour
{
	public static MainMenu Instance { get; private set; } = null;

	[SerializeField]
	private MenuChangeable mainTitle = null;

	[SerializeField]
	private Button[] allButtons = null;

	[SerializeField]
	private MenuChangeable[] allTextsAssociatedWithButtons = null;

	[SerializeField]
	private Transform usedButtonContainer = null;

	[SerializeField]
	private Transform menuButtonContainer = null;

	[SerializeField]
	private GameObject[] allPanelsInMenu = null;

	public readonly Color32 lightEffectColor = new Color(255, 255, 255);
	public readonly Color32 darkEffectColor = new Color(0, 0, 0);

	private Vector2 previousVector;

	protected void Awake()
	{
		Instance = this;
	}

	public Color32 GetReversedColor(Color32 original)
	{
		return (original.Equals(darkEffectColor)) ?
			new Color32(lightEffectColor.r, lightEffectColor.g, lightEffectColor.b, original.a) :
			new Color32(darkEffectColor.r, darkEffectColor.g, darkEffectColor.b, original.a);
	}

	public void ClickPlayBtn()
	{
		BtnAnimation(0, false);
	}

	public void BtnAnimation (int number, bool isReversed)
	{
		if (isReversed)
		{
			allPanelsInMenu[number + 1].SetActive(false);
		}

		allButtons[number].GetComponent<IconHighlighter>().Enable(isReversed);

		allButtons[number].interactable = isReversed;

		allButtons[number].transform.SetParent(isReversed == false ? usedButtonContainer : menuButtonContainer);
		allButtons[number].transform.SetAsFirstSibling();

		allTextsAssociatedWithButtons[number].SpecficGraphic.fontSize = isReversed == false ? 65 : 46;
		string title = isReversed == true ? "Light" : "Dark";
		mainTitle.SpecficGraphic.text = $"<i>{title}</i>  Checkers";


		Vector2 moveVector = new Vector2(mainTitle.transform.position.x + 10,
			mainTitle.transform.position.y - 160);

		Vector2 scaleVector = new Vector2(10, 10);

		if (!isReversed)
		{
			previousVector = allTextsAssociatedWithButtons[number].transform.position;
		}

		else
		{
			moveVector = previousVector;
			scaleVector = new Vector2(1, 1);
		}

		LeanTween.move(allTextsAssociatedWithButtons[number].gameObject, moveVector, 0.1f);
		allTextsAssociatedWithButtons[number].GetComponent<TextPingPong>().enabled = !isReversed;
		LeanTween.scale(allButtons[number].gameObject, scaleVector, 0.4f).setOnComplete(() => allPanelsInMenu[number + 1].SetActive(!isReversed));
		allTextsAssociatedWithButtons[number].transform.SetParent(isReversed == false ? usedButtonContainer : allButtons[number].transform);
		allPanelsInMenu[number].SetActive(isReversed);

	}

	public void ClickOptionsBtn()
	{

	}

	public void ClickCreateBoardBtn()
	{

	}

	public void ClickExitBtn()
	{
		Application.Quit(0);
	}
}
