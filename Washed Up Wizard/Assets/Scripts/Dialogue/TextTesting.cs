using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTesting : MonoBehaviour {

	public float nextCharacterDelay = 0.1f;
	public TextMesh textField;
	public TextAsset testText;

	private int textNum = 0;

	// Use this for initialization
	void Awake () {
		textField.text = "";
		StartCoroutine (WaitForNextCharacter ());
	}

	IEnumerator WaitForNextCharacter () {
		yield return new WaitForSeconds (nextCharacterDelay);
		NextCharacter ();
	}

	void NextCharacter () {
		if (testText.text.Length > textNum) {
			textField.text += testText.text [textNum];
			textNum++;
			StartCoroutine (WaitForNextCharacter ());
		}
	}
}
