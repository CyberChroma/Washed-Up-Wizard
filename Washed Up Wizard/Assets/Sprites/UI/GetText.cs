using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GetText : MonoBehaviour {

    public string CharacterText;

    private string text;



    public string getText (){
        StreamReader getText = new StreamReader(CharacterText);
        string text = getText.ReadToEnd();
        return text;
    }
}
