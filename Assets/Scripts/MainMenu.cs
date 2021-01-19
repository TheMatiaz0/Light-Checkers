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
	private MenuChangeable mainTitle = null, version = null, author = null;

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

	// private Vector2 previousVector;

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

		allTextsAssociatedWithButtons[number].gameObject.SetActive(isReversed);

		string title = isReversed == true ? "Light" : "Dark";
		mainTitle.SpecficGraphic.text = $"<i>{title}</i>  Checkers";


		/*Vector2 moveVector = new Vector2(mainTitle.transform.position.x + 10,
			mainTitle.transform.position.y - 160); */

		Vector2 scaleVector = new Vector2(10, 10);

		if (isReversed)
		{
			scaleVector = new Vector2(1, 1);
		}

		// LeanTween.move(allTextsAssociatedWithButtons[number].gameObject, moveVector, 0.1f);
		LeanTween.scale(allButtons[number].gameObject, scaleVector, 0.3f).setOnComplete(() => allPanelsInMenu[number + 1].SetActive(!isReversed));

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
		author.SetupColor(new Color32((byte)author.GraphicToChange.color.r, (byte)author.GraphicToChange.color.g, (byte)author.GraphicToChange.color.b, 0), new Color32(), 3);
		BtnAnimation(3, false);
		// Application.Quit(0);
	}
}
