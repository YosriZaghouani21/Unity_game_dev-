using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public bool playerInRange;
    public bool isTalkingWithPlayer;

    private bool isWriting;
    private string fullText;
    private string fullText2;
    private int characterIndex;

    private bool showingFullText1; // To track whether the first or second part of the text is currently displayed.

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

    internal void StartConversation()
    {
        isTalkingWithPlayer = true;
        print("Conversation Started");
        DialogueSystem.Instance.OpenDialogUI();
        fullText = "Greetings, traveler lost in time. I am Monejya, your guide through this extraordinary journey. ";
        fullText2 = "You may be feeling disoriented, but fear not; I am here to assist you.";
        StartCoroutine(WriteText(fullText));
    }

    private IEnumerator WriteText(string text)
    {
        isWriting = true;
        characterIndex = 0;
        string displayedText = "";

        while (characterIndex < text.Length)
        {
            displayedText += text[characterIndex];
            characterIndex++;

            // Update the TextMeshPro text component
            DialogueSystem.Instance.dialogtext.text = displayedText;

            yield return new WaitForSeconds(0.05f); // You can adjust the time delay between characters.
        }

        isWriting = false;

        if (text == fullText)
        {
            DialogueSystem.Instance.option2BTN.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = "Read More";
            DialogueSystem.Instance.option2BTN.onClick.AddListener(() =>
            {
                showingFullText1 = false;
                StartCoroutine(WriteText(fullText2));
            });
        }
        else
        {
            DialogueSystem.Instance.option1BTN.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = "Bye";
            DialogueSystem.Instance.option1BTN.onClick.AddListener(() =>
            {
                DialogueSystem.Instance.CloseDialogUI();
                isTalkingWithPlayer = false;
            });
        }
    }
}
