using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clickmescript : MonoBehaviour
{
    public Transform Key4; // Reference to the 3D GameObject (Key4)
    public Button clickme; // Reference to the UI Button (clickme)

    private RectTransform buttonRectTransform;

    private void Start()
    {
        // Get the RectTransform component of the UI Button (clickme)
        buttonRectTransform = clickme.GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Check if the objectToFollow is not null
        if (Key4 != null)
        {
            // Get the 3D object's position in world space (Key4)
            Vector3 worldPosition = Key4.position;

            // Convert the world position to screen space
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

            // Set the UI button's position to match the screen position
            buttonRectTransform.position = screenPosition;
        }
    }
}
