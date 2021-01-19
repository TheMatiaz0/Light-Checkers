using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBoxManager : MonoBehaviour
{
	public static InfoBoxManager Instance { get; private set; } = null;

	[SerializeField]
	private GameObject infoBox = null;

	[SerializeField]
	private Text description = null;

	[SerializeField]
	private Image descriptiveImage = null;

	private bool canFollow = false;

	protected void Awake()
	{
		Instance = this;
	}

	protected void Update()
	{
		if (!canFollow)
		{
			return;
		}

		infoBox.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y - 170);
	}

	public void Activate(string descr, Sprite sprite = null)
	{
		canFollow = true;
		description.text = descr;
		if (sprite != null)
			descriptiveImage.sprite = sprite;
		infoBox.SetActive(true);
	}

	public void Deactive()
	{
		infoBox.SetActive(false);
		canFollow = false;
	}
}
