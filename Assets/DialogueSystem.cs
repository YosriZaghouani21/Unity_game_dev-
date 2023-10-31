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

    public bool dialogUIactive;


    private void Awake()
    {
        if(Instance != null && Instance != this)
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
        dialogUIactive.gameObject.SetActive(true) ;
        dialogUIactive = true ;

        Cursor lockState = CursosLockMode.None ;
        Cursor visible = true;
    }


    public void CloseDialogUI()
    {
        dialogUIactive.gameObject.SetActive(false);
        dialogUIactive = false;

        Cursor lockState = CursosLockMode.Locked;
        Cursor visible = false;

    }
}
