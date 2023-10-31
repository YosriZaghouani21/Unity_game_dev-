using UnityEngine;

public class KeyItempickup : MonoBehaviour
{
    private bool isPickedUp = false; // Track whether the key is picked up

    private void Update()
    {
        // Check for the "E" key press
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isPickedUp)
            {
                // Drop the key
                isPickedUp = false;
                gameObject.SetActive(true); // Show the key
                Debug.Log("Key Dropped");
            }
            else
            {
                // If not already picked up, pick up the key
                if (CanPickUpKey())
                {
                    isPickedUp = true;
                    gameObject.SetActive(false); // Hide the key
                    Debug.Log("Key Picked Up");
                }
            }
        }
    }

    private bool CanPickUpKey()
    {
        // Implement your logic here to determine if the key can be picked up.
        // Return true if it's allowed, otherwise return false.
        // You might use distance, triggers, or other criteria here.
        return true; // For demonstration purposes
    }
}
