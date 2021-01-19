using UnityEngine;
using UnityEngine.EventSystems;

public class IconHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField]
	private Color elementsColorAfterHighlight;

	[SerializeField]
	private Color32 shadowColorAfterHighlight;

	private Animator animator = null;

	private Color32 oldColorElements;
	private Color32 oldColorShadow;

	[SerializeField]
	private MenuChangeable[] elementsToHighlight = null;

	private bool disableEverything = false;

	protected void Awake()
	{
		oldColorElements = elementsToHighlight[0].GraphicToChange.color;
		oldColorShadow = elementsToHighlight[0].MeshEffect.effectColor;
		animator = GetComponent<Animator>();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (disableEverything)
		{
			return;
		}

		EnterIcon();
	}

	private void EnterIcon()
	{
		animator.enabled = true;
		foreach (var item in elementsToHighlight)
		{
			item.SetupColor(elementsColorAfterHighlight, shadowColorAfterHighlight, 2.44f);
		}
	}

	public void Enable(bool isTrue = true)
	{
		disableEverything = !isTrue;

		if (!isTrue)
		{
			ExitIcon();
		}
	}

	private void ExitIcon()
	{
		animator.enabled = false;

		foreach (var item in elementsToHighlight)
		{
			LeanTween.cancel(item.gameObject);
			item.SetupColor(oldColorElements, oldColorShadow, 0.5f);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (disableEverything)
		{
			return;
		}
		
		ExitIcon();
	}
}
