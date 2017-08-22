using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HoloToolkit.Unity.InputModule;

public class PlaySoundOnClick : MonoBehaviour, IInputClickHandler {

	public AudioSource audioSource;

	void Start () {
	}
	
	void Update () {
	}

	#region IInputClickHandler implementation

	public void OnInputClicked (InputClickedEventData eventData)
	{
		audioSource.Play();
	}

	#endregion
}
