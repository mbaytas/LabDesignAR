using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HoloToolkit.Unity.InputModule;

public class Miqus_Focus : MonoBehaviour, IFocusable
{

	void Start ()
	{
	}

	void Update ()
	{
	}

	#region IFocusable implementation

	public void OnFocusEnter ()
	{
		gameObject.transform.parent.tag = "MiqusFocus";
	}

	public void OnFocusExit ()
	{
		gameObject.transform.parent.tag = "Untagged";
	}

	#endregion
}
