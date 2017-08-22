using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HoloToolkit.Unity.InputModule;

public class DestroyButton : MonoBehaviour, IInputClickHandler
{

	void Start ()
	{
	}

	void Update ()
	{
	}

	#region IInputClickHandler implementation

	public void OnInputClicked (InputClickedEventData eventData)
	{
		Destroy (gameObject.transform.parent.gameObject);
	}

	#endregion
}
