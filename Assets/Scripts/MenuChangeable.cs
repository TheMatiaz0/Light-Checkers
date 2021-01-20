using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuChangeable : MonoBehaviour
{
	[SerializeField]
	private Graphic graphicToChange = null;

	public Graphic GraphicToChange => graphicToChange;

	[SerializeField]
	private Shadow meshEffect = null;

	public Shadow MeshEffect => meshEffect;

	[SerializeField]
	private Text specificGraphic = null;
	public Text SpecficGraphic => specificGraphic;

	public void SetupColor(Color colorForGraphic, Color32 colorForEffect, float time)
	{
		LeanTween.value(graphicToChange.gameObject, graphicToChange.color, colorForGraphic, time).setOnUpdate((Color val) =>
		{
			graphicToChange.color = val;
		}).setIgnoreTimeScale(true);

		if (meshEffect != null)
		{
			LeanTween.color(meshEffect.gameObject, colorForEffect, time).setIgnoreTimeScale(true);
			LeanTween.value(meshEffect.gameObject, meshEffect.effectColor.a, colorForEffect.a, time).setIgnoreTimeScale(true);
		}
	}
}
