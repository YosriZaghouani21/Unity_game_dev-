using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    // Reference to the InventorySystem script

    public InventorySystem inventorySystem;

    void Start()
    {
        // Try to find the InventorySystem component on the same GameObject
        inventorySystem = GetComponent<InventorySystem>();

        // If the component is not found on this GameObject, try finding it in parents
        if (inventorySystem == null)
        {
            inventorySystem = GetComponentInParent<InventorySystem>();
        }

        if (inventorySystem != null)
        {
            // Attach the OpenInventory method to the button click event
            GetComponent<Button>().onClick.AddListener(inventorySystem.ToggleInventory);
        }
        else
        {
            Debug.LogError("InventorySystem component not found!");
        }
    }


}
