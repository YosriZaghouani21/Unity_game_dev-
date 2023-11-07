using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class KdialogueManager : MonoBehaviour
{
    public TextMeshProUGUI Tname; // For TMP Text in Text Mesh Pro
    public TextMeshProUGUI Tdialogue;
    private Queue<string> sentences; 
    public Animator animatoor;
    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(Kdialogue dialoguee)
    {
        animatoor.SetBool("IsOpen", true);

        Debug.Log("start convo with " + dialoguee.name);
        Tname.text = dialoguee.name;
        sentences.Clear();

        foreach (string sentence in dialoguee.sentences)
        {
            sentences.Enqueue(sentence);
            Debug.Log("Enqueued sentence: " + sentence);
        }
        DisplayNextSentence();
        Debug.Log("testing 1");
    }

    public void DisplayNextSentence()
    {
        Debug.Log("testing 2");
        Debug.Log("Sentences count: " + sentences.Count);

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        Tdialogue.text = sentence;
        Debug.Log(sentence);
    }


    void EndDialogue()
    {
        animatoor.SetBool("IsOpen", false);
        Debug.Log("End of Conversation");

       
    }

}
