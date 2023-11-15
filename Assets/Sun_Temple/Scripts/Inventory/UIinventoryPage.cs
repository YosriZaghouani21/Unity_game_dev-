using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIinventoryPage : MonoBehaviour
{
    [SerializeField]
    private UIinventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private UIinventoryDescription itemDescription;

    [SerializeField]
    private MouseFollower mouseFollower;


    List<UIinventoryItem> listOfUIitems = new List<UIinventoryItem>();

    private int currentlyDraggedItemIndex = -1;

    public event Action<int> OnDescriptionRequested,
    OnItemActionRequested,
    OnStartDragging;

    public event Action<int, int> OnSwapItems;

    private void Awake()
    {
        Hide();
        mouseFollower.Toggle(false);
        itemDescription.ResetDescription();
    }
    public void InitializeInventoryUI(int  inventorysize)
    {
        for (int i = 0; i < inventorysize;  i++)
        {
            UIinventoryItem uiItem =
                Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listOfUIitems.Add(uiItem);
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseBtnClick += HandleShowItemActions;

          
        }

    }
    public void UpdateData(int itemIndex, Sprite itemImage,
        int itemQuantity)
    {
        if (listOfUIitems.Count > itemIndex)
        {
            listOfUIitems[itemIndex].SetData(itemImage, itemQuantity);
        }

    }

    private void HandleShowItemActions(UIinventoryItem inventoryItemUI)
    {
    }

    private void HandleEndDrag(UIinventoryItem inventoryItemUI)
    {
        ResetDraggedItem();

    }

    private void HandleSwap(UIinventoryItem inventoryItemUI)
    {
        int index = listOfUIitems.IndexOf(inventoryItemUI);
        if (index == -1)
        {
            return;
        }
        OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);

    }

    private void ResetDraggedItem()
    {
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1;
    }

    private void HandleBeginDrag(UIinventoryItem inventoryItemUI)
    {
        int index = listOfUIitems.IndexOf(inventoryItemUI);
        if (index == -1)
            return;
        currentlyDraggedItemIndex = index;
        HandleItemSelection(inventoryItemUI);
        OnStartDragging?.Invoke(index);
        }

    public void CreateDraggedItem(Sprite sprite, int quantity)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite, quantity);
    }

    private void HandleItemSelection(UIinventoryItem inventoryItemUI)
    {
        int index = listOfUIitems.IndexOf(inventoryItemUI);
        if (index == -1)
            return; 
        OnDescriptionRequested?.Invoke(index);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        ResetSelection();
    }

    public void ResetSelection()
    {
        itemDescription.ResetDescription();
        DeselectAllItems();
    }

    private void DeselectAllItems()
    {
         foreach (UIinventoryItem item in listOfUIitems)
        {
            item.Deselect();
        }
            }

    public void Hide()
    {
        gameObject.SetActive(false);
        ResetDraggedItem();
    }

    internal void UpdateDescription(int itemindex, Sprite itemImage, string name, string description)
    {
        itemDescription.SetDescription(itemImage, name, description);
        DeselectAllItems();
        listOfUIitems[itemindex].Select();
    }
}
