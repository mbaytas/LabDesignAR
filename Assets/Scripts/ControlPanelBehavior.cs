using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelBehavior : MonoBehaviour
{

	public float radius = 1.0f;
	[Tooltip ("In degrees.")]
	public float angularSpacing = 45.0f;
	public float verticalOffsetFromCamera = -1.2f;

	void Start ()
	{
		// Lay out all the buttons in a circle around the camera
		int childCount = gameObject.transform.childCount;

		float thetaStart = -(childCount - 1) * (angularSpacing / 2.0f);

		for (int i = 0; i < childCount; i++) {
			Transform child = gameObject.transform.GetChild (i);

			float theta = thetaStart + i * angularSpacing;

			child.localPosition = new Vector3 (
				radius * Mathf.Sin (theta * Mathf.Deg2Rad),
				0,
				radius * Mathf.Cos (theta * Mathf.Deg2Rad)
			);

			child.localRotation = Quaternion.Euler (
				75,
				theta,
				0
			);
		}
	}

	void Update ()
	{
		// Translate
		Vector3 camPos = Camera.main.transform.position;

		gameObject.transform.position = new Vector3 (
			camPos.x,
			camPos.y + verticalOffsetFromCamera,
			camPos.z
		);
	}
}
