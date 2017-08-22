using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HoloToolkit.Unity.InputModule;

public class ControlPanel_ChangeConeLengthButton : MonoBehaviour, IInputClickHandler {

	public float delta = 0.2f;

	void Start () {
	}

	void Update () {
	}

	#region IInputClickHandler implementation

	public void OnInputClicked (InputClickedEventData eventData)
	{
		GameObject[] cones = GameObject.FindGameObjectsWithTag("Viewcone");

		// We want all viewcones to be same length, use first one as reference
		Vector3 initScale = cones[0].transform.localScale;

		// Rescale all viewcones
		foreach (GameObject cone  in cones) {
			float yScale = initScale.y + delta;
			float xScale = initScale.x * (yScale / initScale.y);
			float zScale = initScale.z * (yScale / initScale.y);

			cone.transform.localScale = new Vector3 (xScale, yScale, zScale);
		}
	}

	#endregion
}
