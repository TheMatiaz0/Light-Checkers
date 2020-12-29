using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheats : MonoBehaviour
{
	protected void Update()
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			SceneManager.LoadScene("Main");
		}
	}
}
