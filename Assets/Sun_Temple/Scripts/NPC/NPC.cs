// NPC script
using System.Collections;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public bool playerInRange;
    public bool isTalkingWithPlayer;
    private NPCMovementAI npcMovementAI;

    private bool isWriting;
    private string[] fullTexts;
    private int currentTextIndex;
    private int characterIndex;

    private enum NPCState
    {
        Idle,
        FirstConversation,
        SecondConversation
    }

    private NPCState currentState;

    private void Start()
    {
        // Assign the NPCMovementAI component from the same GameObject
        npcMovementAI = GetComponent<NPCMovementAI>();
        if (npcMovementAI == null)
        {
            Debug.LogWarning("NPCMovementAI component not found on the same GameObject.");
        }

        // Set the initial state to Idle
        currentState = NPCState.Idle;
    }

    private void EndConversation()
    {
        DialogueSystem.Instance.CloseDialogUI();
        isTalkingWithPlayer = false;

        // Reset the "Read More" button to its default state.
        DialogueSystem.Instance.option1BTN.onClick.RemoveAllListeners();
        DialogueSystem.Instance.option1BTN.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = "Read More";
        DialogueSystem.Instance.option1BTN.gameObject.SetActive(true);

        // Here we call MoveToLevel1, but first check if npcMovementAI is not null
        if (npcMovementAI != null)
        {
            // Update the state based on the current state
            switch (currentState)
            {
                case NPCState.FirstConversation:
                    // Move to the first destination
                    npcMovementAI.MoveToLevel1();
                    break;

                case NPCState.SecondConversation:
                    // Move to the new destination
                    npcMovementAI.MoveToNewDestination();
                    break;

                default:
                    break;
            }
        }
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

        // Update the state based on the current state
        switch (currentState)
        {
            case NPCState.Idle:
                // Start the first conversation
                StartFirstConversation();
                break;

            case NPCState.FirstConversation:
                // Start the second conversation
                StartSecondConversation();
                break;

            case NPCState.SecondConversation:
                // Handle additional states if needed
                break;

            default:
                break;
        }
    }

    private void StartFirstConversation()
    {
        DialogueSystem.Instance.OpenDialogUI();
        fullTexts = new string[]
        {
          "Greetings, Traveler lost in time. I am Monejya, your guide through this extraordinary journey.",
            "You may be feeling disoriented, but fear not I am here to assist you.",
            "You stand amidst the Medina of Tunis in the year 1900. Your quest is to recover lost artifacts.",
            "But be mindful of the clues hidden within these ancient streets. Start by examining the carpet over there."
        };

        currentTextIndex = 0;
        StartCoroutine(WriteText(fullTexts[currentTextIndex]));
        currentState = NPCState.FirstConversation;
    }

    private void StartSecondConversation()
    {
        DialogueSystem.Instance.OpenDialogUI();
        fullTexts = new string[]
        {
            "This is the second conversation text.",
            // ... (other second conversation texts)
        };

        currentTextIndex = 0;
        StartCoroutine(WriteText(fullTexts[currentTextIndex]));
        currentState = NPCState.SecondConversation;
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