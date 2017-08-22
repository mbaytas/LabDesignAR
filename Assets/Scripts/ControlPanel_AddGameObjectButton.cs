using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HoloToolkit.Unity.InputModule;

public class ControlPanel_AddGameObjectButton : MonoBehaviour, IInputClickHandler
{

	public GameObject prefab;
	public float scale = 1.0f;

	void Start ()
	{
	}

	void Update ()
	{
	}

	#region IInputClickHandler implementation

	public void OnInputClicked (InputClickedEventData eventData)
	{
		GameObject go = Instantiate (prefab);
		go.transform.localScale = new Vector3 (scale, scale, scale);
	}

	#endregion
}
