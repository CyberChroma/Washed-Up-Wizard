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
            if (!dialogue.hasTask)
            {
                gameObject.SetActive(false);
            } else if (dialogue.taskComplete) {
                GetComponentInChildren<Animator>().SetTrigger("Activate");
            }
        }
    }

    public void Update () {
        if (dialogue.hasTask)
        {
            dialogue.taskComplete = true;
            for (int i = 0; i < dialogue.items.Length; i++)
            {
                if (dialogue.items[i] && dialogue.items[i].activeSelf)
                {
                    dialogue.taskComplete = false;
                }
            }
        }
        dialogue.getSentences();
    }
}
