using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberevolver.Unity;
using UnityEngine.UI;
using Cyberevolver;
using System.Diagnostics;
using System;

public class TimerController : MonoBehaviour
{
    public static TimerController Instance { get; private set; } = null;

    [SerializeField]
    private Text countdownText = null;

    [SerializeField]
    private Text secondCountdown = null;

    [SerializeField]
    private TimeSpan timeEnd = new TimeSpan(0, 5, 0);

    [SerializeField]
    private Animator timerAnimator = null;

	protected void Awake()
	{
        Instance = this;
    }

    public void SetupCountdown(Player[] players)
	{
		foreach (var item in players)
		{
            item.Time = (float)timeEnd.TotalSeconds;
		}
	}

    public void ChangeCountdown(Player currentPlayer)
	{
        StopAllCoroutines();
        timerAnimator.SetBool("team", !timerAnimator.GetBool("team"));

        Text t = null;


        if (currentPlayer == GameManager.Players[0])
		{
            t = secondCountdown;
		}

        else if (currentPlayer == GameManager.Players[1])
		{
            t = countdownText;
		}

        StartCoroutine(StartCountdown(currentPlayer, t, (x) => Change(ref currentPlayer.Time, x)));
	}

    private IEnumerator StartCountdown(Player currentPlayer, Text t, Action<float> action)
	{
		for (float i = currentPlayer.Time; i > 0; i -= 0.01f)
		{
            t.text = TimeSpan.FromSeconds(i).ToString(@"mm\:ss");
            action(i);
            yield return new WaitForSeconds(0.01f);
        }
	}

    private void Change(ref float whichOne, float newVal)
	{
        whichOne = newVal;
	}
}
