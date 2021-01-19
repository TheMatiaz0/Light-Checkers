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
	private GameObject buttonContainer = null;

	[SerializeField]
	private Transform usedButtonContainer = null;

	public readonly Color32 lightEffectColor = new Color(255, 255, 255);
	public readonly Color32 darkEffectColor = new Color(0, 0, 0);

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
		allButtons[0].GetComponent<IconHighlighter>().Disable();
		allButtons[0].interactable = false;

		allButtons[0].image.color = Color.white;
		allButtons[0].transform.SetParent(usedButtonContainer);
		allTextsAssociatedWithButtons[0].transform.SetParent(usedButtonContainer);

		// allTextsAssociatedWithButtons[0].SetupColor(GetReversedColor(allTextsAssociatedWithButtons[0].GraphicToChange.color), 
			// GetReversedColor(allTextsAssociatedWithButtons[0].MeshEffect.effectColor), 0.44f);

		allTextsAssociatedWithButtons[0].SpecficGraphic.fontSize = 65;
		mainTitle.SpecficGraphic.text = "<i>Dark</i>  Checkers";

		buttonContainer.SetActive(false);

		LeanTween.move(allTextsAssociatedWithButtons[0].gameObject, new Vector2(mainTitle.transform.position.x + 10,
			mainTitle.transform.position.y - 160), 0.1f);
		LeanTween.scale(allButtons[0].gameObject, new Vector2(10, 10), 0.4f);
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
