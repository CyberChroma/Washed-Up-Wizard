using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {
    
    public bool hasTask;
    public GameObject[] items;
    public string name; // Name of character
    public TextAsset[] textReferences; // References to text files
 
    [HideInInspector] public bool taskComplete = false;
    [HideInInspector] public string[] sentences;

    public void getSentences () {
        if (hasTask)
        {
            if (!taskComplete)
            {
                sentences = textReferences[0].text.Split('\n');
            }
            else
            {
                sentences = textReferences[1].text.Split('\n');
            }
        }
        else
        {
            sentences = textReferences[0].text.Split('\n');
        }
    }
}
