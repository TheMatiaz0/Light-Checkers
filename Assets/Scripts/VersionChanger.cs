using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersionChanger : MonoBehaviour
{
	private Text versionText = null;

	protected void Awake()
	{
		versionText = GetComponent<Text>();
		versionText.text = string.Format(versionText.text, Application.version);
	}
}
