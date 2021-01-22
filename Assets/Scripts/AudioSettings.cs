using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class AudioSettings
{
	public static void SetVolume(AudioMixer mixer, float value, string prefsName)
	{
		mixer.SetFloat(prefsName, Mathf.Log(value) * 20);
		// PlayerPrefs.SetFloat();
	}
}
