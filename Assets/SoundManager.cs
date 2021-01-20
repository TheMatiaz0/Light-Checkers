using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberevolver;
using Cyberevolver.Unity;
using System.Linq;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance { get; private set; } = null;

	private List<AudioClip> usedWalkSounds = new List<AudioClip>();

	[SerializeField]
	private AudioSource specialSource = null;

	[SerializeField]
	private AudioClip[] walkingSounds = null;

	[SerializeField]
	private AudioClip upgradeSound = null;

	protected void Awake()
	{
		Instance = this;
		usedWalkSounds = walkingSounds.ToList();
	}

	public void PlayKill()
	{
		specialSource.PlayOneShot(upgradeSound);
	}

	public void PlayWalk ()
	{
		if (usedWalkSounds.Count < 1)
		{
			usedWalkSounds = walkingSounds.ToList();
		}

		AudioClip clip = usedWalkSounds[Random.Range(0, usedWalkSounds.Count)];
		specialSource.PlayOneShot(clip);
		usedWalkSounds.Remove(clip);
	}
}
