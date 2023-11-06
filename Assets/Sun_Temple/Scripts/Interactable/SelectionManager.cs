using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public bool OnTarget;
    public GameObject interaction_Info_UI;
    Text interaction_text;
    public GameObject selectedObject;
    private void Start()
    {
        interaction_text = interaction_Info_UI.GetComponent<Text>();
        interaction_Info_UI.SetActive(false);
        OnTarget = false;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;
            InteractableObject interactable  = selectionTransform.GetComponent<InteractableObject>();
            NPC npc = selectionTransform.GetComponent<NPC>();
                if (npc && npc.playerInRange)
                {
                    interaction_text.text = "Talk";
                    interaction_Info_UI.SetActive(true);
                  
                    if (Input.GetMouseButton(0) && npc.isTalkingWithPlayer == false)   //CHANGE TO TACTILE
                    {
                        npc.StartConversation();
                    }
                    if (DialogueSystem.Instance.dialogUIActive)
                    {
                        interaction_Info_UI.SetActive(false);
                    }
                }
                else
                {
                    interaction_text.text = "";
                    interaction_Info_UI.SetActive(true);
                }
            if (interactable != null && interactable.playerInRange)
            {
                interaction_text.text = interactable.GetItemName();
                interaction_Info_UI.SetActive(true);
                selectedObject = interactable.gameObject;
                OnTarget = true;
            }
            else
            {
                interaction_Info_UI.SetActive(false);
                OnTarget = false;
            }

        }
        else
        {
            interaction_Info_UI.SetActive(false);
        }

    }
}
