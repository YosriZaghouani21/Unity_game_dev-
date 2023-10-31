using System.Collections;
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
    public Image handIcon;
    public bool handIsVisible;
    public GameObject selectedTree;
    public GameObject chopHolder;
    // Start is called before the first frame update
    void Start()
    {
        onTarget = false;
        interaction_text = interaction_Info_UI.GetComponent<Text>();  
    }
    private void Awake()
    {
        if (Instance !=null && Instance != this)
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
        
    }
}
