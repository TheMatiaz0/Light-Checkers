using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	private CanvasGroup mainUI = null;

	[SerializeField]
	private MeshRenderer canvasTripod = null;

	private MeshRenderer[] allTripods = null;

	protected void Awake()
	{
		allTripods = canvasTripod.GetComponentsInChildren<MeshRenderer>();
	}

	protected void Update()
	{
		if (Input.GetKeyDown(KeyCode.F1))
		{
			if (mainUI.alpha <= 0)
			{
				LeanTween.alphaCanvas(mainUI, 1, 1);

				foreach (var item in allTripods)
				{
					item.enabled = true;
				}

			}

			else
			{
				LeanTween.alphaCanvas(mainUI, 0, 1);
				foreach (var item in allTripods)
				{
					item.enabled = false;
				}
			}


		}
	}
}
