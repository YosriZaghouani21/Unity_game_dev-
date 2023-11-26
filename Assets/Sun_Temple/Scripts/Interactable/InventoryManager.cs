using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    private Dictionary<string, PickItems> itemsCollected = new Dictionary<string, PickItems>();

    public void AddItem(string itemTag, PickItems itemScript)
    {
        if (!itemsCollected.ContainsKey(itemTag))
        {
            itemsCollected.Add(itemTag, itemScript);
        }
    }

    public bool HasItem(string itemTag)
    {
        return itemsCollected.ContainsKey(itemTag);
    }

    public PickItems RemoveItem(string itemTag)
    {
        if (itemsCollected.TryGetValue(itemTag, out PickItems itemScript))
        {
            itemsCollected.Remove(itemTag);
            return itemScript;
        }
        return null;
    }
}