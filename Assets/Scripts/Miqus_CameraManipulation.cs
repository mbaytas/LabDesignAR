using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using HoloToolkit.Unity.InputModule;

public class Miqus_CameraManipulation : MonoBehaviour, IManipulationHandler
{

	public float scale_y = 5.0f;
	public float scale_yaw = -200.0f;

	private GameObject parentGameObject;
	private GameObject rodGameObject;
	private Vector3 parentInitLocalPosition;
	private Vector3 parentInitLocalRotation_euler;
	private Transform rodInitTransform;

	void Start ()
	{
	}

	void Update ()
	{
	}

	#region IManipulationHandler implementation

	public void OnManipulationStarted (ManipulationEventData eventData)
	{
		// Save initial state
		parentGameObject = gameObject.transform.parent.gameObject;
		parentInitLocalPosition = parentGameObject.transform.localPosition;
		parentInitLocalRotation_euler = parentGameObject.transform.localRotation.eulerAngles;

		rodGameObject = parentGameObject.transform.parent.Find ("Rod").gameObject;
		rodInitTransform = rodGameObject.transform;

		// https://github.com/Microsoft/HoloToolkit-Unity/issues/709
		InputManager.Instance.ClearModalInputStack ();
		InputManager.Instance.PushModalInputHandler (gameObject);
	}

	public void OnManipulationUpdated (ManipulationEventData eventData)
	{
		// Persist gaze focus indicator
		gameObject.transform.Find ("GazeFocusIndicator").gameObject.SetActive (true);

		// Make sure the thing doesn't move too low, out of sight
		if (parentGameObject.transform.position.y < 3) {
			
			// Update y / height ...
			float delta_y = eventData.CumulativeDelta.y * scale_y;

			// ... of camera and viewcone
			parentGameObject.transform.localPosition = parentInitLocalPosition + new Vector3 (0, delta_y, 0);

			// ... of rod
			rodGameObject.transform.localPosition = new Vector3 (
				rodInitTransform.localPosition.x,
				parentGameObject.transform.localPosition.y / 2,
				rodInitTransform.localPosition.z
			);
			rodGameObject.transform.localScale = new Vector3 (
				rodInitTransform.localScale.x,
				parentGameObject.transform.localPosition.y / 2,
				rodInitTransform.localScale.z
			);

			// Update yaw / rotation around y of camera and viewcome
			float delta_yaw = eventData.CumulativeDelta.x * scale_yaw;
			Vector3 delta_yaw_euler = new Vector3 (0, delta_yaw, 0);
			parentGameObject.transform.localRotation = Quaternion.Euler (parentInitLocalRotation_euler + delta_yaw_euler);
		}

	}

	public void OnManipulationCompleted (ManipulationEventData eventData)
	{
		Kill ();
	}

	public void OnManipulationCanceled (ManipulationEventData eventData)
	{
		Kill ();
	}

	#endregion

	private void Kill ()
	{
		// https://github.com/Microsoft/HoloToolkit-Unity/issues/709
		InputManager.Instance.ClearModalInputStack ();

		// Kill gaze focus indicator
		gameObject.transform.Find ("GazeFocusIndicator").gameObject.SetActive (false);
	}
}
