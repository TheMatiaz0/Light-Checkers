using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	private CanvasGroup mainUI = null;

	protected void Update()
	{
		if (Input.GetKeyDown(KeyCode.F1))
		{
			mainUI.alpha = mainUI.alpha == 0 ? 1 : 0;
		}
	}
}
