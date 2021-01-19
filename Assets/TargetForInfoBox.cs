using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetForInfoBox : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[TextArea]
	[SerializeField]
	private string description = null;

	[SerializeField]
	private Sprite descrSprite = null;

	public void OnPointerEnter(PointerEventData eventData)
	{
		InfoBoxManager.Instance.Activate(description, descrSprite);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		InfoBoxManager.Instance.Deactive();
	}
}
