using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPingPong : MonoBehaviour
{
	[SerializeField]
	private float time;

	[SerializeField]
	private Vector2 maxScale;

	[SerializeField]
	private Vector2 minScale;


	protected void Start()
	{
		LeanTween.scale(gameObject, maxScale, time).setOnComplete(() => LeanTween.scale(gameObject, minScale, time) ).setLoopPingPong();
	}
}
