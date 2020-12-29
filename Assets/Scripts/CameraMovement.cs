using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	private Transform cam;
	private Transform parent;

	private Vector3 localRotation;
	private float cameraDistance = 10f;

	[SerializeField]
	private float mouseSensitivity = 4f;
	[SerializeField]
	private float scrollSensitivity = 2f;
	[SerializeField]
	private float orbitDampening = 10f;
	[SerializeField]
	private float scrollDampening = 6f;

	[SerializeField]
	private float maxScrollOut = 100f; 
	[SerializeField]
	private float minScrollOut = 1.5f;

	public bool LockRotation { get; set; } = false;

	protected void Start()
	{
		cam = this.transform;
		parent = this.transform.parent;
		localRotation.y = 40f;
	}

	protected void Update()
	{
		// Reset position of your camera.
		if (Input.GetKeyDown(KeyCode.R))
		{
			AdjustCamera(GameManager.Instance.CurrentPlayer.CurrentTeam);
		}
	}

	public void AdjustCamera(Team specificTeam)
	{
		LockRotation = true;
		if (specificTeam == Team.Black)
		{
			localRotation.x = 0;
		}

		else
		{
			localRotation.x = 180;
		}

		LockRotation = false;
	}

	protected void LateUpdate()
	{
		if (LockRotation)
		{
			return;
		}

		if (Input.GetMouseButton(1))
		{
			localRotation.x += Input.GetAxis("Mouse X") * mouseSensitivity;
			localRotation.y -= Input.GetAxis("Mouse Y") * mouseSensitivity;

			localRotation.y = Mathf.Clamp(localRotation.y, 0, 90);
		}

		float scrollAmount = Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;

		scrollAmount *= (this.cameraDistance * 0.3f);
		cameraDistance += scrollAmount * -1f;
		cameraDistance = Mathf.Clamp(cameraDistance, minScrollOut, maxScrollOut);


		Quaternion qt = Quaternion.Euler(localRotation.y, localRotation.x, 0);
		parent.rotation = Quaternion.Lerp(parent.rotation, qt, Time.deltaTime * orbitDampening);
		cam.localPosition = new Vector3(0, 0, Mathf.Lerp(cam.localPosition.z, cameraDistance * -1f, Time.deltaTime * scrollDampening));
	}
}
