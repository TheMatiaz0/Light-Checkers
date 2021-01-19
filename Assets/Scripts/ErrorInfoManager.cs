using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorInfoManager : MonoBehaviour
{
	public static ErrorInfoManager Instance { get; private set; } = null;

	protected void Awake()
	{
		Instance = this;
	}

	[SerializeField]
	private Text playerOneError = null;

	public Text PlayerOneError => playerOneError;

	[SerializeField]
	private Text playerTwoError = null;
	public Text PlayerTwoError => playerTwoError;

	[SerializeField]
	private Text dropdownError = null;

	public Text DropdownError => dropdownError;
}
