using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HoloToolkit.Unity.InputModule;

public class ControlPanel_ToggleLogButton : MonoBehaviour, IInputClickHandler
{
	public GameObject consoleGameObject;

	void Start () {
	}

	void Update () {
	}

	#region IInputClickHandler implementation

	public void OnInputClicked (InputClickedEventData eventData)
	{
		if (consoleGameObject.activeSelf) {
			consoleGameObject.SetActive (false);
		} else {
			consoleGameObject.SetActive (true);
		}
	}

	#endregion
}
