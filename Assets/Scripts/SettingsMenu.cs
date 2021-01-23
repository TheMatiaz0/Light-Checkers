using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
	[SerializeField]
	private Dropdown resolutionDropdown = null;

	[SerializeField]
	private Toggle vSyncToggle = null;

	[SerializeField]
	private Dropdown qualityDropdown = null;

	[SerializeField]
	private Toggle fullscreenToggle = null;

	[SerializeField]
	private Slider[] audioSliders = null;

	[SerializeField]
	private Transform deco = null;

	[SerializeField]
	private Transform background = null;

	private Resolution[] resolutions = null;

	[SerializeField]
	private AudioMixer mixer;

	[SerializeField]
	private Toggle woodenFrame = null;

	protected void OnEnable()
	{
		background.localScale = new Vector2(0, 0);
		deco.localScale = new Vector2(0, 0);
		LeanTween.scale(background.gameObject, new Vector2(1, 1), 0.35f);
		LeanTween.scale(deco.gameObject, new Vector2(1, 1), 0.4f);
	}

	protected void Awake()
	{
		if (resolutionDropdown != null)
		{
			LoadResolution();
		}

		woodenFrame.isOn = !GameManager.HideWoodenPlatforms;
		qualityDropdown.value = QualitySettings.GetQualityLevel();
		vSyncToggle.isOn = QualitySettings.vSyncCount == 1 ? true : false;
		fullscreenToggle.isOn = Screen.fullScreen;
		audioSliders[0].value = 1;
		audioSliders[1].value = 1;
		audioSliders[2].value = 1;
	}

	private void LoadResolution()
	{
		resolutions = Screen.resolutions.SkipWhile(x => x.width < 1000).ToArray();
		resolutionDropdown.ClearOptions();
		List<string> allOptions = new List<string>();
		int resIndex = 0;
		for (int i = 0; i < resolutions.Length; i++)
		{
			string option = $"{resolutions[i].width}x{resolutions[i].height}, {resolutions[i].refreshRate}Hz";
			allOptions.Add(option);
			if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height && resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
			{
				resIndex = i;
			}
		}

		resolutionDropdown.AddOptions(allOptions);
		resolutionDropdown.value = resIndex;
		resolutionDropdown.RefreshShownValue();
	}

	public void SetFullscreen(bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
	}

	public void SetQuality(int qualityIndex)
	{
		QualitySettings.SetQualityLevel(qualityIndex);
	}

	public void SetResolution(int resolutionIndex)
	{
		Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreen, resolutions[resolutionIndex].refreshRate);
	}

	public void SetVSync(bool isVSync)
	{
		QualitySettings.vSyncCount = isVSync == true ? 1 : 0;
	}

	public void SetMasterVolume(float value)
	{
		AudioSettings.SetVolume(mixer, value, "masterVol");
	}

	public void SetMusicVolume(float value)
	{
		AudioSettings.SetVolume(mixer, value, "musicVol");
	}

	public void SetSFXVolume(float value)
	{
		AudioSettings.SetVolume(mixer, value, "sfxVol");
	}

	public void SetDeco(bool isTrue)
	{
		GameManager.HideWoodenPlatforms = !isTrue;
	}

	public void BackBtn()
	{
		LeanTween.scale(background.gameObject, new Vector2(0, 0), 0.25f).setOnComplete(() => MainMenu.Instance.BtnAnimation(2, true));
	}
}
