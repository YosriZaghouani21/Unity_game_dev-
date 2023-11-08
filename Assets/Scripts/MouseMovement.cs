using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 250f;

    float xRotation = 0f;
    float YRotation = 0f;
    private Transform playerTransform; // Reference to the player's transform

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerTransform = transform.parent; // Assuming the player's transform is the parent
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        YRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerTransform.localRotation = Quaternion.Euler(0f, YRotation, 0f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
