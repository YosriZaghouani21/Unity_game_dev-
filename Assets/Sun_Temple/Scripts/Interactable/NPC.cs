using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{

    public bool playerInRange;
    public bool isTalkingWithPlayer;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
            StartConversation();
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
            EndConversation();
        }


    }

    public void StartConversation()
    {
        isTalkingWithPlayer = true;

        print("Conversation Started");

        DialogueSystem.Instance.OpenDialogUI();
        DialogueSystem.Instance.dialogtext.text = "Hello There";
        DialogueSystem.Instance.option1BTN.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = "Bye";
        DialogueSystem.Instance.option1BTN.onClick.AddListener(() =>
        {
            DialogueSystem.Instance.CloseDialogUI();
            isTalkingWithPlayer = false;
        });
    }

    public void EndConversation()
    {
        isTalkingWithPlayer = false;
        DialogueSystem.Instance.CloseDialogUI();
        print("Conversation Ended");
   
    }



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}