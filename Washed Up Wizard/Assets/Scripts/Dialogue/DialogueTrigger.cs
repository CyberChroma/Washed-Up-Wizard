using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public Dialogue dialogue;

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag ("Player"))
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            if (dialogue.tasktype == Dialogue.TaskType.None)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void Update () {
        dialogue.getSentences();
    }
}
