using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private HashSet<string> itemsCollected = new HashSet<string>();

    public void AddItem(string itemTag)
    {
        itemsCollected.Add(itemTag);
    }
    public bool HasItem(string itemTag)
    {
        return itemsCollected.Contains(itemTag);
    }
    public void RemoveItem(string itemTag)
    {
        itemsCollected.Remove(itemTag);
    }
}
