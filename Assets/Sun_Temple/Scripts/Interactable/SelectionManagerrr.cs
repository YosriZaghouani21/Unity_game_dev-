/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; private set; }
    public bool onTarget;
    public GameObject selectedObject;
    public GameObject interaction_Info_UI;
    Text interaction_text;
    public Image centerDotImage;
    // Start is called before the first frame update
    void Start()
    {
        onTarget = false;
        interaction_text = interaction_Info_UI.GetComponent<Text>();
    }
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
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;
            PickItems interactable = selectionTransform.GetComponent<PickItems>();
            NPC npc = selectionTransform.GetComponent<NPC>();
            if (selectionTransform.GetComponent<InteractableObject>())
            {
                interaction_text.text = selectionTransform.GetComponent<InteractableObject>().GetItemName();
                interaction_Info_UI.SetActive(true);
                if (npc && npc.playerInRange)
                {
                    interaction_text.text = "Talk";
                    interaction_Info_UI.SetActive(true);
                    //CHAAAAAANGE TO TACTILE
                    if (Input.GetKey(KeyCode.X) && npc.isTalkingWithPlayer == false)
                    {
                        Debug.Log("X key pressed and conversation starting.");
                        npc.StartConversation();
                    }
                    if (DialogueSystem.Instance.dialogUIActive)
                    {
                        interaction_Info_UI.SetActive(false);
                    }
                }
                else
                {
                    interaction_text.text = "AAAAAAAAAAAAAAAAA";
                    interaction_Info_UI.SetActive(false);
                }
                if (interactable && interactable.playerInRange)
                {
                    onTarget = true;
                    selectedObject = interactable.gameObject;
                    interaction_Info_UI.SetActive(true);
                    if (interactable.CompareTag("pickable"))
                    {
                        centerDotImage.gameObject.SetActive(false);
                    }
                    else
                    {
                        onTarget = false;
                        interaction_Info_UI.SetActive(false);
                        centerDotImage.gameObject.SetActive(true);
                    }
                }
                else
                {
                    onTarget = false;
                    interaction_Info_UI.SetActive(false);

                }
            }
        }
        else
        {
            interaction_Info_UI.SetActive(false);
        }
    }
}
*/