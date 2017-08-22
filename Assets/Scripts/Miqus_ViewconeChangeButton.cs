using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HoloToolkit.Unity.InputModule;

public class Miqus_ViewconeChangeButton : MonoBehaviour, IInputClickHandler
{
	public bool createOnStart;
	public float horizontalFOVInDegrees;
	public float verticalFOVInDegrees;
	public float viewconeLengthInMeters = 2.0f;

	void Start ()
	{
		if (createOnStart) {
			UpdateViewcone ();
		}

	}

	#region IInputClickHandler implementation

	public void OnInputClicked (InputClickedEventData eventData)
	{
		UpdateViewcone ();
	}

	#endregion

	void UpdateViewcone ()
	{
		GameObject parentGameObject = gameObject.transform.parent.gameObject;
		GameObject miqusAndViewconeGameObject = parentGameObject.transform.Find ("MiqusAndViewcone").gameObject;
		GameObject viewconeLowPolyGameObject = miqusAndViewconeGameObject.transform.Find ("Viewcone_LowPoly").gameObject;

		if (viewconeLowPolyGameObject != null && viewconeLowPolyGameObject.activeSelf) {
			ViewconesManager.DrawViewcone (viewconeLowPolyGameObject, horizontalFOVInDegrees, verticalFOVInDegrees);
		} else {
			Debug.Log ("Something is wrong: change button cannot find active viewcone game object.");
		}
	}
}