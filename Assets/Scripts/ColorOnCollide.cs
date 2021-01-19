using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorOnCollide : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		MenuChangeable changeable = null;
		if (changeable = collision.GetComponent<MenuChangeable>())
		{
			Color32 effectColor = new Color32();
			if (changeable.MeshEffect != null)
			{
				effectColor = changeable.MeshEffect.effectColor;
			}
			changeable.SetupColor(MainMenu.Instance.GetReversedColor(changeable.GraphicToChange.color), 
				MainMenu.Instance.GetReversedColor(effectColor), 0.2f);
		}
	}
}
