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

public class RigifyToUnityPrefs {
	
	private const string c_NameFilterKey = "RigifyToUnityNameFilter";
	private const string c_NameFilterDefault = "Rigify";
	public static string NameFilter {
		get {
			return (EditorPrefs.GetString (c_NameFilterKey, c_NameFilterDefault));
		}
		set {
			EditorPrefs.SetString (c_NameFilterKey, value);
		}
	}
	
	private const string c_BoneShapePrefixKey = "RigifyToUnityBoneShapePrefix";
	private const string c_BoneShapePrefixDefault = "WGT";
	public static string BoneShapePrefix {
		get {
			return (EditorPrefs.GetString (c_BoneShapePrefixKey, c_BoneShapePrefixDefault));
		}
		set {
			EditorPrefs.SetString (c_BoneShapePrefixKey, value);
		}
	}
	
	private const string c_ExceptionBonesKey = "RigifyToUnityExceptionBone";
	private const string c_ExceptionBonesDefault = "Position";
	public static string ExceptionBones {
		get {
			return (EditorPrefs.GetString (c_ExceptionBonesKey, c_ExceptionBonesDefault));
		}
		set {
			EditorPrefs.SetString (c_ExceptionBonesKey, value);
		}
	}
	
	public static void UseDefaultEditorPrefs () {
		NameFilter = c_NameFilterDefault;
		BoneShapePrefix = c_BoneShapePrefixDefault;
		ExceptionBones = c_ExceptionBonesDefault;
	}
}
