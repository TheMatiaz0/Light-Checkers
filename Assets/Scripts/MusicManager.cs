using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	public static MusicManager Instance { get; private set; } = null;

	[SerializeField]
	private AudioSource source = null;

	protected void Awake()
	{
		Instance = this;
	}

	public void ChangeMusic(AudioClip toMusic)
	{
		if (toMusic == source.clip)
		{
			return;
		}

		source.clip = toMusic;
		source.Play();
	}
}
