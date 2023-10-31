using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{

    public static DialogueSystem Instance { get; set; }

    public TextMeshProUGUI dialogtext;

    public Button option1BTN;
    public Button option2BTN;

    public Canvas dialogUI;

    public GameObject dialogUIactive;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void OpenDialogUI()
    {
        // Make sure the dialogUIactive reference is not null before trying to access its gameObject property
        if (dialogUIactive != null)
        {
            // Activate the dialog UI (assuming dialogUIactive is a GameObject)
            dialogUIactive.SetActive(true);
        }
        else
        {
            Debug.LogError("dialogUIactive is not assigned in the Inspector.");
        }

        // Adjust the cursor settings
        Cursor.lockState = CursorLockMode.None; // This should be CursorLockMode, not CursosLockMode
        Cursor.visible = true;
    }


    public void CloseDialogUI()
    {
        // Make sure the dialogUIactive reference is not null before trying to access its gameObject property
        if (dialogUIactive != null)
        {
            // Deactivate the dialog UI (assuming dialogUIactive is a GameObject)
            dialogUIactive.SetActive(false);
        }
        else
        {
            Debug.LogError("dialogUIactive is not assigned in the Inspector.");
        }

        // Adjust the cursor settings
        Cursor.lockState = CursorLockMode.Locked; // This should be CursorLockMode, not CursosLockMode
        Cursor.visible = false;
    }
}