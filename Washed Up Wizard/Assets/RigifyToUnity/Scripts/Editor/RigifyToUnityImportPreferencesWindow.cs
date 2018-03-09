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
using System.Collections;

public class RigifyToUnityImportPreferencesWindow : EditorWindow {
	
	private const string c_WindowTitle = "Rigify to Unity";
	private GUIContent m_NameFilterLabel;
	private GUIContent m_BoneShapePrefixLabel;
	private GUIContent m_ExceptionBonesLabel;
	
	private void OnEnable () {
		m_NameFilterLabel = new GUIContent ("Name Filter", "Models with names that contain '" + RigifyToUnityPrefs.NameFilter + "', will go through the Rigify to Unity post processor. The name filter is not case sensitive.");
		m_BoneShapePrefixLabel = new GUIContent ("Shape Prefix", "Rigify automatically generates bone shape meshes in Blender that are imported to Unity. The game objects containing those meshes have the prefix '" + RigifyToUnityPrefs.BoneShapePrefix + "'. All those meshes are destroyed and the corresponding game objects as well, if possible. The shape prefix search is not case sensitive.");
		m_ExceptionBonesLabel = new GUIContent ("Exception Bones", "Bones with names that contain '" + RigifyToUnityPrefs.ExceptionBones + "', will not be deleted, even if they are not needed by any skinned mesh. The exception bones search is not case sensitive.");
	}
	
	private void OnGUI () {
		this.title = c_WindowTitle;
		GUILayout.Label ("Model Name Filter", EditorStyles.boldLabel);
		RigifyToUnityPrefs.NameFilter = EditorGUILayout.TextField (m_NameFilterLabel, RigifyToUnityPrefs.NameFilter);
		
		EditorGUILayout.Space ();
		
		GUILayout.Label ("Bone Shape Prefix", EditorStyles.boldLabel);
		RigifyToUnityPrefs.BoneShapePrefix = EditorGUILayout.TextField (m_BoneShapePrefixLabel, RigifyToUnityPrefs.BoneShapePrefix);
		
		EditorGUILayout.Space ();
		
		GUILayout.Label ("Special Bones", EditorStyles.boldLabel);
		RigifyToUnityPrefs.ExceptionBones = EditorGUILayout.TextField (m_ExceptionBonesLabel, RigifyToUnityPrefs.ExceptionBones);
		
		EditorGUILayout.Space ();
		
		EditorGUILayout.BeginHorizontal ();
		if (GUILayout.Button ("Use Defaults")) {
			RigifyToUnityPrefs.UseDefaultEditorPrefs ();
		}
		GUILayout.FlexibleSpace ();
		EditorGUILayout.EndHorizontal ();
	}
	
	[MenuItem ("Window/Blender/Rigify to Unity...")]
	private static void CreateConfigurationWindow () {
		EditorWindow.GetWindow (typeof (RigifyToUnityImportPreferencesWindow));
	}
}
