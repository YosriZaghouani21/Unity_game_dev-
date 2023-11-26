using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 150f;

    float xRotation = 0f;
    float YRotation = 0f;

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
        //Locking the cursor to the middle of the screen and making it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!DialogueSystem.Instance.dialogUIActive )
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //control rotation around x axis (Look up and down)
        xRotation -= mouseY;

        //we clamp the rotation so we cant Over-rotate (like in real life)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //control rotation around y axis (Look up and down)
        YRotation += mouseX;

        //applying both rotations
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
