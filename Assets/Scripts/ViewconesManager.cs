using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HoloToolkit.Unity;

public class ViewconesManager : Singleton<ViewconesManager>
{
	public static void UpdateViewconeCollection (GameObject containerGameObject,
	                                    Vector3[] positions,
	                                    Vector3[] rotations,
	                                    float horizontalFOVInDegrees,
	                                    float verticalFOVInDegrees)
	{
		// Check how many
		if (positions.Length != rotations.Length) {
			string s = string.Format(
				"Arrays for positions ({0}) and rotations ({1}) must be same length!",
				positions.Length,
				rotations.Length
			);
			Debug.Log (s);
			return;
		}
		int n = positions.Length;

		Debug.Log ("Updating viewcones...");
		// Create and reset container for all viewcones
		Debug.Log ("Destroying existing viewcones...");
		// Clear it out
		foreach (Transform child in containerGameObject.transform) {
			GameObject.Destroy (child.gameObject);
		}

		Debug.Log ("Creating new viewcones...");
		// Instantiate a faces container for every viewcone
		for (int coneIdx = 0; coneIdx < n; coneIdx++) {
			GameObject facesContainerGameObject = new GameObject ();
			Instantiate (facesContainerGameObject);
			// Reset transform
			facesContainerGameObject.transform.parent = containerGameObject.transform;
			facesContainerGameObject.transform.localPosition = positions [coneIdx];
			facesContainerGameObject.transform.localRotation = Quaternion.Euler (rotations [coneIdx]);
			facesContainerGameObject.transform.localScale = Vector3.one;

			// DEBUG
			string dbug_cams = string.Format (
				                   "Set viewcone {0} @ {1} {2} {3} / {4} {5} {6}.",
				                   coneIdx.ToString ("0"),
				                   positions [coneIdx].x.ToString ("0.00"),
				                   positions [coneIdx].y.ToString ("0.00"),
				                   positions [coneIdx].z.ToString ("0.00"),
				                   rotations [coneIdx].x.ToString ("0.00"),
				                   rotations [coneIdx].y.ToString ("0.00"),
				                   rotations [coneIdx].z.ToString ("0.00")

			                   );
			Debug.Log (dbug_cams);
			//// DEBUG

			DrawViewcone (facesContainerGameObject, horizontalFOVInDegrees, verticalFOVInDegrees);


		}
	}

	public static void DrawViewcone (GameObject facesContainerGameObject, 
	                                float horizontalFOVInDegrees, 
	                                float verticalFOVInDegrees)
	{
		// Clear it out
		foreach (Transform child in facesContainerGameObject.transform) {
			GameObject.Destroy (child.gameObject);
		}

		// Define vertex coords
		Vector3[] vertices = new Vector3[4];
		float viewConeLength = 2.0f; // Just init with 2m cones, resize later
		vertices [0] = new Vector3 (
			viewConeLength * Mathf.Tan (Mathf.Deg2Rad * horizontalFOVInDegrees / 2.0f),
			viewConeLength * Mathf.Tan (Mathf.Deg2Rad * verticalFOVInDegrees / 2.0f),
			-viewConeLength
		);
		vertices [1] = new Vector3 (
			-viewConeLength * Mathf.Tan (Mathf.Deg2Rad * horizontalFOVInDegrees / 2.0f),
			viewConeLength * Mathf.Tan (Mathf.Deg2Rad * verticalFOVInDegrees / 2.0f),
			-viewConeLength
		);
		vertices [2] = new Vector3 (
			-viewConeLength * Mathf.Tan (Mathf.Deg2Rad * horizontalFOVInDegrees / 2.0f),
			-viewConeLength * Mathf.Tan (Mathf.Deg2Rad * verticalFOVInDegrees / 2.0f),
			-viewConeLength
		);
		vertices [3] = new Vector3 (
			viewConeLength * Mathf.Tan (Mathf.Deg2Rad * horizontalFOVInDegrees / 2.0f),
			-viewConeLength * Mathf.Tan (Mathf.Deg2Rad * verticalFOVInDegrees / 2.0f),
			-viewConeLength
		);

		// Instatiate and populate GameObjects for each face
		for (int faceIdx = 0; faceIdx < 4; faceIdx++) {
			GameObject viewconeFaceGameObject = new GameObject ();
			Instantiate (viewconeFaceGameObject);
			// Reset transform
			viewconeFaceGameObject.transform.parent = facesContainerGameObject.transform;
			viewconeFaceGameObject.transform.localPosition = Vector3.zero;
			viewconeFaceGameObject.transform.localRotation = Quaternion.identity;
			viewconeFaceGameObject.transform.localScale = Vector3.one;

			// Mesh filter
			MeshFilter meshFilter = viewconeFaceGameObject.AddComponent<MeshFilter> ();

			// Mesh renderer
			MeshRenderer meshRenderer = viewconeFaceGameObject.AddComponent<MeshRenderer> ();
			Material lowPolyViewconeMaterial = Resources.Load ("ViewconeLinesMaterial", typeof(Material)) as Material;
			meshRenderer.material = lowPolyViewconeMaterial;

			// Assign coords to meshes	
			Mesh mesh = meshFilter.mesh;
			mesh.SetVertices (new List<Vector3> () {
				Vector3.zero,
				vertices [faceIdx],
				vertices [(faceIdx + 1) % 4]
			});
			mesh.triangles = new int[] { 0, 1, 2 };
		}
	}
}
