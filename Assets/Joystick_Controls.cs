using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class Joystick_Controls : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Transform _cameraTransform;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _gravity = 9.8f;

    private void Update()
    {
        HandlePlayerMovement();
    }

    private void HandlePlayerMovement()
    {
        // Handle player movement
        Vector3 moveDirection = new Vector3(_joystick.Horizontal, 0f, _joystick.Vertical).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + _cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _rotationSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveVector = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // Apply gravity
            moveVector.y -= _gravity * Time.deltaTime;

            // Use SimpleMove for smooth movement and built-in collision handling
            _characterController.SimpleMove(moveVector.normalized * _moveSpeed);
        }
    }
}
