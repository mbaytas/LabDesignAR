using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDPResponse_Myo : MonoBehaviour
{
	public float Scale_Pitch = 1.0f;
	public float Scale_Roll = -1.0f;

	private GameObject miqusAndViewconeInFocus = null;

	public void ResponseToUDPPacket (string incomingIP, string incomingPort, byte[] data)
	{
		string msgString = System.Text.Encoding.UTF8.GetString (data);
		Debug.Log ("UDP: " + msgString);

		// Split with null delimiter means split at spaces
		string[] msgArr = msgString.Split (null);

		switch (msgArr [0]) {
		case "MYOARM":
			FocusOnMiqus ();
			break;
		case "MYOREL":
			ReleaseMiqus ();
			break;
		case "MYODAT":
			RotateMiqus (msgArr);
			break;
		default:
			break;
		}
	}

	public void FocusOnMiqus ()
	{
		miqusAndViewconeInFocus = GameObject.FindGameObjectWithTag ("MiqusFocus");
	}

	public void ReleaseMiqus ()
	{
		miqusAndViewconeInFocus = null;
	}

	public void RotateMiqus (string[] msgArr)
	{
		float deltaPitch = float.Parse (msgArr [1]);
		float deltaRoll = float.Parse (msgArr [2]);
		// float deltaYaw = float.Parse (msgArr [3]);

		// Make sure the thing doesn't rotate too much
		float prx = miqusAndViewconeInFocus.transform.localRotation.eulerAngles.x;
		if ((prx > 0 && prx < 90) || (prx > 270 && prx < 360)) {
			Vector3 delta_rot_euler = new Vector3 (
				                          deltaPitch * Scale_Pitch,
				                          0,
				                          deltaRoll * Scale_Roll);
			miqusAndViewconeInFocus.transform.Rotate (delta_rot_euler);
		}
	}
}