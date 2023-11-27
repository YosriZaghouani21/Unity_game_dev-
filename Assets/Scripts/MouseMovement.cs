using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 150f;

    float xRotation = 0f;
    float YRotation = 0f;
    private Transform playerTransform; // Reference to the player's transform

    public static MouseMovement Instance { get; private set; }

    void Awake()
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
        Cursor.lockState = CursorLockMode.Locked;
        playerTransform = transform.parent; // Assuming the player's transform is the parent
    }

    void Update()
    {
        if (!DialogueSystem.Instance.dialogUIActive)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            YRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            playerTransform.localRotation = Quaternion.Euler(0f, YRotation, 0f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            // Control rotation around Y axis (Look left and right)
            YRotation += mouseX;

            // Applying both rotations
            transform.localRotation = Quaternion.Euler(xRotation, YRotation, 0f);
        }
    }

    public void StopMouseLook()
    {
        Cursor.lockState = CursorLockMode.None;  // Free the cursor
        Cursor.visible = true;  // Make cursor visible
        enabled = false;  // Disable this script
    }

    public void ResumeMouseLook()
    {
        Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor back
        Cursor.visible = false;  // Make cursor invisible
        enabled = true;  // Re-enable the script
    }
}
