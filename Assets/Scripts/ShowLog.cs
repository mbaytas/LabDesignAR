using UnityEngine;

// https://forums.hololens.com/discussion/708/how-can-i-see-the-unity-debug-log-output-from-a-running-app-on-the-device
public class ShowLog : MonoBehaviour
{
	public TextMesh textMesh;

	void Start ()
	{
	}

	void OnEnable()
	{
		Application.logMessageReceived += LogMessage;
	}

	void OnDisable()
	{
		Application.logMessageReceived -= LogMessage;
	}

	public void LogMessage(string message, string stackTrace, LogType type)
	{
		if (textMesh.text.Length > 600)
		{
			textMesh.text = message + "\n";
		}
		else
		{
			textMesh.text += message + "\n";
		}
	}
}