using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HoloToolkit.Unity.InputModule;

public class GazeFocusIndication : MonoBehaviour, IFocusable
{
	public GameObject GazeFocusVisualsGameObject;
	public Color GazeFocusTextColor = Color.cyan;
	public bool AlwaysVisible = false;

	private TextMesh textMesh;
	private Color initTextColor;
	private TextMesh indicatorTextMesh;
	private Color initIndicatorTextColor;

	public void Start ()
	{
		if (GazeFocusVisualsGameObject != null)
		{
			if (AlwaysVisible) {
				GazeFocusVisualsGameObject.SetActive (true);
			} else {
				GazeFocusVisualsGameObject.SetActive (false);
			}
		}

		textMesh = gameObject.GetComponent (typeof(TextMesh)) as TextMesh;
		indicatorTextMesh = GazeFocusVisualsGameObject.GetComponent(typeof(TextMesh)) as TextMesh;
		if (textMesh != null)
		{
			initTextColor = textMesh.color;
		}
		if (indicatorTextMesh != null)
		{
			initIndicatorTextColor = indicatorTextMesh.color;
		}
	}

	#region IFocusable implementation
	public void OnFocusEnter ()
	{
		if (GazeFocusVisualsGameObject != null)
		{
			GazeFocusVisualsGameObject.SetActive (true);
		}

		if (textMesh != null)
		{
			textMesh.color = GazeFocusTextColor;
		}
		if (indicatorTextMesh != null)
		{
			indicatorTextMesh.color = GazeFocusTextColor;
		}
	}
	public void OnFocusExit ()
	{
		if (GazeFocusVisualsGameObject != null)
		{
			if (!AlwaysVisible) {
				GazeFocusVisualsGameObject.SetActive (false);
			}
		}

		if (textMesh != null)
		{
			textMesh.color = initTextColor;
		}
		if (indicatorTextMesh != null)
		{
			indicatorTextMesh.color = initIndicatorTextColor;
		}
	}
	#endregion
}
