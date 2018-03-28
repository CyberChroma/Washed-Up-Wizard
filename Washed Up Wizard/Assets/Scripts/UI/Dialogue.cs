using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {

    public enum TaskType
    {
        _pickup,
        _killEnemies
    }

    System.Random rand = new System.Random(); 

    public TaskType tasktype;
    public Pickup pickup;
    public killEnemies killEnemies;
    public string name;//name of character
    public TextAsset[] textReferences;//references to text files

    private int numOfLines;
    private int whichSentence;

    [HideInInspector]public string[] sentences;
    [HideInInspector]public bool thankYou = false;

    public void getSentences (){
        numOfLines = textReferences.Length;
        if (tasktype == TaskType._pickup)
        {
            if (pickup.taskComplete == false)
            {
                sentences = textReferences[numOfLines - 1].text.Split('\n');
            }
            else if (pickup.taskComplete == true && thankYou == false)
            {
                sentences = textReferences[numOfLines - 2].text.Split('\n');
            }
            else if (pickup.taskComplete == true && thankYou == true)
            {
                sentences = textReferences[rand.Next(0, numOfLines - 3)].text.Split('\n');
            }
        }
        else if (tasktype == TaskType._killEnemies)
        {
            if (killEnemies.taskComplete == false)
            {
                sentences = textReferences[numOfLines - 1].text.Split('\n');
            }
            else if (killEnemies.taskComplete == true && thankYou == false)
            {
                sentences = textReferences[numOfLines - 2].text.Split('\n');
            }
            else if (killEnemies.taskComplete == true && thankYou == true)
            {
                sentences = textReferences[rand.Next(0, numOfLines - 3)].text.Split('\n');
            }
        }
    }
}
