using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnDepthBuffer : MonoBehaviour
{
	void Start ()
	{
		// https://chrismflynn.wordpress.com/2012/09/06/fun-with-shaders-and-the-depth-buffer/
		Camera.main.depthTextureMode = DepthTextureMode.Depth;
	}

	void Update ()
	{
	}
}
