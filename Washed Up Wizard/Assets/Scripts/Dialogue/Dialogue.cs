﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {

    public enum TaskType
    {
        None,
        Pickup,
        KillEnemies
    }

    public TaskType tasktype;
    public GameObject[] pickups;
    public KillEnemies killEnemies;
    public string name; // Name of character
    public TextAsset[] textReferences; // References to text files

    private int numOfLines;
    private int whichSentence;

    [HideInInspector] public bool taskComplete = false;
    [HideInInspector] public string[] sentences;
    [HideInInspector] public bool thankYou = false;

    public void getSentences () {
        numOfLines = textReferences.Length;
        if (tasktype == TaskType.Pickup)
        {
            if (taskComplete == false)
            {
                sentences = textReferences[0].text.Split('\n');
            }
            else if (taskComplete == true && thankYou == false)
            {
                sentences = textReferences[1].text.Split('\n');
            }
            else if (taskComplete == true && thankYou == true)
            {
                sentences = textReferences[Random.Range(2, numOfLines)].text.Split('\n');
            }
        }
        else if (tasktype == TaskType.KillEnemies)
        {
            if (killEnemies.taskComplete == false)
            {
                sentences = textReferences[0].text.Split('\n');
            }
            else if (killEnemies.taskComplete == true && thankYou == false)
            {
                sentences = textReferences[1].text.Split('\n');
            }
            else if (killEnemies.taskComplete == true && thankYou == true)
            {
                sentences = textReferences[Random.Range(2, numOfLines)].text.Split('\n');
            }
        }
        else
        {
            sentences = textReferences[0].text.Split('\n');
        }
    }
}
