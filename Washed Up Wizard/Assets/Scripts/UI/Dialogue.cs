using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {

    public string name;
    public int numOfLines;

    [TextArea(3,100)]
    public string[] sentences;
}
