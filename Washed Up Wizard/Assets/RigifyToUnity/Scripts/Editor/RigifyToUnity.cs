//
// Author:
//   Andreas Suter (andy@edelweissinteractive.com)
//
// Copyright (C) 2011-2012 Edelweiss Interactive (http://edelweissinteractive.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class RigifyToUnity : AssetPostprocessor {
	
	private void OnPostprocessModel (GameObject a_GameObject) {
		
		if (a_GameObject.name.ToLower ().Contains (RigifyToUnityPrefs.NameFilter.ToLower ())) {
			
				// Needed game objects.
			
			HashSet <GameObject> l_NeededGameObjects = new HashSet <GameObject> ();
			l_NeededGameObjects.Add (a_GameObject);
			
				// All meshes are needed, except for the ones that are used as bone shapes in Blender.
			foreach (MeshRenderer l_MeshRenderer in a_GameObject.GetComponentsInChildren <MeshRenderer> ()) {
				if (!l_MeshRenderer.name.ToLower ().StartsWith (RigifyToUnityPrefs.BoneShapePrefix.ToLower ())) {
					l_NeededGameObjects.Add (l_MeshRenderer.gameObject);
				} else {
						// Game object is going to be deleted later. But we already destroy the mesh.
					MeshFilter l_MeshFilter = l_MeshRenderer.GetComponent <MeshFilter> ();
					if (l_MeshFilter != null && l_MeshFilter.sharedMesh != null) {
						Object.DestroyImmediate (l_MeshFilter.sharedMesh, true);
					}
				}
			}
			
				// All skinned meshes are needed.
			foreach (SkinnedMeshRenderer l_SkinnedMeshRenderer in a_GameObject.GetComponentsInChildren <SkinnedMeshRenderer> ()) {
				l_NeededGameObjects.Add (l_SkinnedMeshRenderer.gameObject);
				foreach (Transform l_BoneTransform in l_SkinnedMeshRenderer.bones) {
					l_NeededGameObjects.Add (l_BoneTransform.gameObject);
				}
			}
			
				// All game objects that contain 'ExceptionBones' in the name are needed.
			foreach (Transform l_Transform in a_GameObject.GetComponentsInChildren <Transform> ()) {
				if (l_Transform.name.ToLower ().Contains (RigifyToUnityPrefs.ExceptionBones)) {
					l_NeededGameObjects.Add (l_Transform.gameObject);
				}
			}
			
				// All parents of the needed game objects can not be deleted.
			List <GameObject> l_NeededGameObjectsWithoutParents = new List <GameObject> (l_NeededGameObjects);
			foreach (GameObject l_NeededObject in l_NeededGameObjectsWithoutParents) {
				Transform l_Transform = l_NeededObject.transform;
				while (l_Transform.parent != null) {
					l_Transform = l_Transform.parent;
					l_NeededGameObjects.Add (l_Transform.gameObject);
				}
			}
			
			
				// Not needed game objects.
			
			HashSet <GameObject> l_NotNeededGameObjects = new HashSet <GameObject> ();
			foreach (Transform l_Transform in a_GameObject.GetComponentsInChildren <Transform> ()) {
				l_NotNeededGameObjects.Add (l_Transform.gameObject);
			}
			l_NotNeededGameObjects.ExceptWith (l_NeededGameObjects);
			
				// Delete not needed game objects as long as the set is not empty.
			while (l_NotNeededGameObjects.Count != 0) {
				List <GameObject> l_NotNeededGameObjectsForLoop = new List <GameObject> (l_NotNeededGameObjects);
				foreach (GameObject l_NotNeededGameObject in l_NotNeededGameObjectsForLoop) {
					if (l_NotNeededGameObject.transform.childCount == 0) {
						l_NotNeededGameObjects.Remove (l_NotNeededGameObject);
						Object.DestroyImmediate (l_NotNeededGameObject, true);
					}
				}
			}
		}
	}
}
