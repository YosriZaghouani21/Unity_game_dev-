using UnityEngine;
using UnityEngine.UI;

public class FixedMenuButton : MonoBehaviour
{
    private RectTransform rectTransform;
    public Image menuImage; // Reference to the UI Image component

    // Set the padding from the left edge of the screen
    public float leftPadding = 20f;

    void Start()
    {
        // Get the RectTransform of the button within the Canvas
        rectTransform = GetComponent<RectTransform>();

        // Anchor the button to the left side of the screen
        rectTransform.anchorMin = new Vector2(0f, 0.5f);
        rectTransform.anchorMax = new Vector2(0f, 0.5f);
        rectTransform.pivot = new Vector2(0f, 0.5f);

        // Set the initial position based on left padding
        rectTransform.anchoredPosition = new Vector2(leftPadding, 0f);

        // Ensure the button maintains its position on screen resize
        ResizeButton();

        // Ensure the image is visible initially
        if (menuImage != null)
        {
            menuImage.enabled = true;
        }
    }

    void Update()
    {
        // Ensure the button remains in the correct position on screen resize
        ResizeButton();
    }

    void ResizeButton()
    {
        // Get the current screen dimensions
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);

        // Calculate the new position based on left padding and current screen size
        float normalizedLeftPadding = leftPadding / screenSize.x;
        float newXPosition = normalizedLeftPadding * Screen.width;

        // Update the button's position
        rectTransform.anchoredPosition = new Vector2(newXPosition, rectTransform.anchoredPosition.y);
    }

    // Method to toggle the visibility of the image
    public void ToggleImageVisibility()
    {
        if (menuImage != null)
        {
            menuImage.enabled = !menuImage.enabled;
        }
    }
}
