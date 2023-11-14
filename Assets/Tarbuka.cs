using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tarbuka : MonoBehaviour
{
    public bool playerInRange;
    public bool isTalkingWithPlayer;
    private NPCMovementAI npcMovementAI;

    private bool isWriting;
    private string[] fullTexts;
    private int currentTextIndex;
    private int characterIndex;

    private void Start()
    {

    }

    private void EndConversation()
    {
        DialogueSystem.Instance.CloseDialogUI();
        isTalkingWithPlayer = false;

        // Reset the "Read More" button to its default state.
        DialogueSystem.Instance.option1BTN.onClick.RemoveAllListeners();
        DialogueSystem.Instance.option1BTN.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = "Read More";
        DialogueSystem.Instance.option1BTN.gameObject.SetActive(true);


    }



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
        fullTexts = new string[]
        {
"Tabuka"        ,
        };

        currentTextIndex = 0;
        StartCoroutine(WriteText(fullTexts[currentTextIndex]));
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

        if (currentTextIndex < fullTexts.Length - 1)
        {
            SetReadMoreButton("Read More", fullTexts[currentTextIndex + 1]);
        }
        else
        {
            DialogueSystem.Instance.option2BTN.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = "Bye";
            DialogueSystem.Instance.option2BTN.onClick.AddListener(() =>
            {
                EndConversation();
            });
        }
    }

    private void SetReadMoreButton(string buttonText, string nextText)
    {
        // Clear all existing listeners to avoid stacking up multiple listeners.
        DialogueSystem.Instance.option1BTN.onClick.RemoveAllListeners();
        DialogueSystem.Instance.option2BTN.onClick.RemoveAllListeners();

        // Set the text for the "Read More" button.
        DialogueSystem.Instance.option1BTN.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = buttonText;

        // Add the new listener for the next text.
        DialogueSystem.Instance.option1BTN.onClick.AddListener(() =>
        {
            if (isWriting)
            {
                // If the text is currently being written out, stop that and display the full text immediately.
                StopAllCoroutines();
                DialogueSystem.Instance.dialogtext.text = nextText;
                isWriting = false;
            }
            else
            {
                // If not writing, start the coroutine to write the next text.
                currentTextIndex++;
                StartCoroutine(WriteText(fullTexts[currentTextIndex]));
            }

            // Hide the "Read More" button on the last text.
            if (currentTextIndex == fullTexts.Length - 1)
            {
                DialogueSystem.Instance.option1BTN.gameObject.SetActive(false);
            }
            else
            {
                // Show the "Read More" button for subsequent texts.
                DialogueSystem.Instance.option1BTN.gameObject.SetActive(true);
            }
        });
    }

}
