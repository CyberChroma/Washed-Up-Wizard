using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public Dialogue dialogue;

    private Animator anim;

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag ("Player"))
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            if (dialogue.tasktype == Dialogue.TaskType.None)
            {
                gameObject.SetActive(false);
            } else if (dialogue.taskComplete) {
                GetComponentInChildren<Animator>().SetTrigger("Activate");
            }
        }
    }

    public void Update () {
        if (dialogue.tasktype == Dialogue.TaskType.Pickup)
        {
            dialogue.taskComplete = true;
            for (int i = 0; i < dialogue.pickups.Length; i++)
            {
                if (dialogue.pickups[i] && dialogue.pickups[i].activeSelf)
                {
                    dialogue.taskComplete = false;
                }
            }
        }
        dialogue.getSentences();
    }
}
