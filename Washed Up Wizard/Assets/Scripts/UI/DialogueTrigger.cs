using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public Dialogue dialogue;

    public void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }

    public void OnTriggerExit(Collider other){
        if (other.tag == "Player")
        {
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
    }

    public void Update (){
        dialogue.getSentences();
    }
 
}
