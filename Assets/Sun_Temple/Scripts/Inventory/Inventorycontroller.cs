using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventorycontroller : MonoBehaviour
{
    [SerializeField]
    private UIinventoryPage inventoryUI;

    [SerializeField]
    private InventorySO inventoryData;

    public void Start()
    {
        PrepareUi();
        //inventoryData.Initialize();
    }

    private void PrepareUi()
    {
        inventoryUI.InitializeInventoryUI(inventoryData.Size);
        this.inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
        this.inventoryUI.OnSwapItems += HandleSwapItems;
        this.inventoryUI.OnStartDragging += HandleDragging;
        this.inventoryUI.OnItemActionRequested += HandleItemActionRequest;
    }

    private void HandleItemActionRequest(int itemindex)
    {
    }

    private void HandleDragging(int itemindex)
    {
    }

    private void HandleSwapItems(int itemindex1, int itemindex2)
    {
    }

    private void HandleDescriptionRequest(int itemindex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemindex);
        if (!inventoryItem.IsEmpty)
            return;
                    ItemSO item = inventoryItem.item;
            inventoryUI.UpdateDescription(itemindex, item.ItemImage, item.name, item.Description);
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    inventoryUI.UpdateData(item.Key, 
                     item.Value.item.ItemImage, 
                     item.Value.quantity);
                }
            }
            else
            {
                inventoryUI.Hide();
            }
        }
    }
}
