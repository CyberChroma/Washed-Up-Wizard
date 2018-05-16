using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    private Text nameText;
    private Text dialogueText;
    private int sentenceNum;

    [HideInInspector] public Animator animator;

    private Queue<string> sentences;

	// Use this for initialization
	void Start () {
        nameText = transform.Find("Name Text").GetComponent<Text>();
        dialogueText = transform.Find("Dialogue Text").GetComponent<Text>(); 
        animator = GetComponent<Animator>();
        sentences = new Queue <string>();
	}
	
    public void StartDialogue (Dialogue dialogue) {
        animator.SetBool("Is Open", true);
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence () {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence) {
        dialogueText.text = "";
        for (int i = 0; i < sentence.Length; i+=2)
        {
            if (i + 2 < sentence.Length + 1)
            {
                dialogueText.text += sentence.Substring(i, 2);
            }
            else
            {
                dialogueText.text += sentence.Substring(i, 1);
            }
            yield return new WaitForSeconds (0.02f) ;
        }
    }

    public void EndDialogue() {
         animator.SetBool("Is Open", false);
    }
}
