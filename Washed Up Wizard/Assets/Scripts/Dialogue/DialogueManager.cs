using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    private Text nameText;
    private Text dialogueText;
    private int sentenceNum;

    [HideInInspector] public Animator animator;

    private string[] sentences;

    // Use this for initialization
	void Start () {
        nameText = transform.Find("Name Text").GetComponent<Text>();
        dialogueText = transform.Find("Dialogue Text").GetComponent<Text>(); 
        animator = GetComponent<Animator>();
	}
	
    public void StartDialogue (string name, string[] dialogueSentences) {
        animator.SetBool("Is Open", true);
        nameText.text = name;
        sentenceNum = 0;
        sentences = dialogueSentences;
        DisplayNextSentence();
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            DisplayNextSentence();
        }
    }

    public void DisplayNextSentence () {
        if (sentences.Length - 1 < sentenceNum)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences[sentenceNum];
        dialogueText.text = sentence;
        sentenceNum++;
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
            yield return new WaitForSeconds (0.02f);
        }
    }

    public void EndDialogue() {
         animator.SetBool("Is Open", false);
    }
}
