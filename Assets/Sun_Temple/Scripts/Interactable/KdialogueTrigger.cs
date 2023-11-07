using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KdialogueTrigger : MonoBehaviour
{
    public Kdialogue dialoguee;
    public void TriggerDialogue ()
    {
        Debug.Log("TriggerDialogue called");
        FindObjectOfType<KdialogueManager>().StartDialogue(dialoguee);
    }
}
