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
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }


    }

    public void StartConversation()
    {
        isTalkingWithPlayer = true;

        print("Conversation Started");

        // Assuming you have a reference to the 'DialogueSystem' instance
        DialogueSystem dialogueSystem = DialogueSystem.Instance;

        // Set the text of the dialog
        dialogueSystem.dialogtext.text = "Hello There";

        // Continue with other code
        dialogueSystem.option1BTN.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = "Bye";
        dialogueSystem.option1BTN.onClick.AddListener(() =>
        {
            dialogueSystem.CloseDialogUI();
            isTalkingWithPlayer = false;
        });
    }



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}