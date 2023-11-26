using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI;
    public bool isOpen;
    public List<string> itemList = new List<string>();
    public List<GameObject> slotList = new List<GameObject>();
    private GameObject itemToAdd;
    private GameObject WhatSlotToEquip;
    //public bool isFull;
    public float shakeDetectionThreshold = 2.0f;
    private float accelerometerUpdateInterval = 1.0f / 60.0f;
    private float lowPassKernelWidthInSeconds = 1.0f;
    private float lowPassFilterFactor;
    private Vector3 lowPassValue;

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

    void Start()
    {
        isOpen = false;
        PopulateSlotList();
        lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        lowPassValue = Input.acceleration;


    }

    private void PopulateSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }

  void Update()
    {
        Vector3 acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
        Vector3 deltaAcceleration = acceleration - lowPassValue;

        if (Mathf.Abs(deltaAcceleration.x) > shakeDetectionThreshold && isOpen)
        {
            Debug.Log("Shake detected on X axis, closing inventory");
            inventoryScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }


        Debug.Log("Update is called");

        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
            Debug.Log("I is pressed and inventory is not open");
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;
        }
         if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            Debug.Log("I is pressed and inventory is open");
            inventoryScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }


    }








    public void AddToInventory(string itemName)
     {
         WhatSlotToEquip = FindNextEmptySlot();
         itemToAdd = Instantiate(Resources.Load<GameObject>(itemName), WhatSlotToEquip.transform.position, WhatSlotToEquip.transform.rotation);
         itemToAdd.transform.SetParent(WhatSlotToEquip.transform);
         itemList.Add(itemName);
     } 
  /* 
    public void AddToInventory(string itemName)
    {
        WhatSlotToEquip = FindNextEmptySlot();

        // Log the name of the item you are trying to add
        Debug.Log("Trying to add item: " + itemName);

        // Load the prefab
        GameObject prefab = Resources.Load(itemName) as GameObject;
        if (prefab != null)
        {
            // Log success
            Debug.Log("Prefab loaded successfully: " + prefab.name);

            // Instantiate and set parent
            itemToAdd = Instantiate(prefab, WhatSlotToEquip.transform.position, WhatSlotToEquip.transform.rotation);
            itemToAdd.transform.SetParent(WhatSlotToEquip.transform);

            // Add to the item list
            itemList.Add(itemName);
        }
        else
        {
            // Log failure
            Debug.LogError("Failed to load prefab: " + itemName);
        }
    }
  */

    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in slotList)
        {
            Debug.Log("Checking slot: " + slot.name + " Child count: " + slot.transform.childCount);
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }

        // Return null if no empty slot is found
        return null;
    }




    public bool CheckIfFull()
 {
     int counter = 0;
     foreach (GameObject slot in slotList)
     {
         if (slot.transform.childCount >0)
         {
             counter += 1;
         }
     }

     if (counter == 15)
     {
         return true;
     }
     else { 
         return false; }
 }
   
}  