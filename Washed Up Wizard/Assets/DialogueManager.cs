using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {


    public Text nameText;
    public Text dialogueText;

    public Animator animator;
    private int sentenceNum;

    private Queue<string> sentences;

	// Use this for initialization
	void Start () {
        sentences = new Queue <string>();
	}
        
	
    public void StartDialogue (Dialogue dialogue){

        animator.SetBool("Is Open", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);

        }

        DisplayNextSentence();
        if (dialogue.pickup.taskComplete == true && dialogue.thankYou == false)
        {
            dialogue.thankYou = true;
        } else if (dialogue.killEnemies.taskComplete == true && dialogue.thankYou == false)
        {
            dialogue.thankYou = true;
        }
    }

    public void DisplayNextSentence (){
        if (sentences.Count == 0){
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;


        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence){
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()){
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue(){
         animator.SetBool("Is Open", false);
    }
}