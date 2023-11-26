using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunTemple
{
    public class CharController_Motor : MonoBehaviour
    {
        public float speed = 10.0f;
        public float sensitivity = 60.0f;
        CharacterController character;
        public GameObject cam;
        float moveFB, moveLR;
        float rotHorizontal, rotVertical;
        public bool webGLRightClickRotation = true;
        float gravity = -9.8f;

        // Store the initial position and rotation
        private Vector3 initialPosition;
        private Quaternion initialRotation;

        void Start()
        {
            character = GetComponent<CharacterController>();
            webGLRightClickRotation = false;

            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                webGLRightClickRotation = true;
                sensitivity = sensitivity * 1.5f;
            }

            // Store the initial position and rotation
            initialPosition = transform.position;
            initialRotation = transform.rotation;
        }

        // Add a method to set the player's position and rotation
        public void SetPlayerPositionAndRotation(Vector3 position, Quaternion rotation)
        {
            // Set the player's position and rotation based on loaded data
            if (character != null)
            {
                character.enabled = false;
                transform.position = position;
                transform.rotation = rotation;
                character.enabled = true;
            }
            else
            {
                Debug.LogError("CharacterController is missing.");
            }
        }

        void FixedUpdate()
        {
            moveFB = Input.GetAxis("Horizontal") * speed;
            moveLR = Input.GetAxis("Vertical") * speed;

            rotHorizontal = Input.GetAxisRaw("Mouse X") * sensitivity;
            rotVertical = Input.GetAxisRaw("Mouse Y") * sensitivity;

            Vector3 movement = new Vector3(moveFB, gravity, moveLR);
            movement = transform.rotation * movement;

            if (webGLRightClickRotation)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    CameraRotation(cam, rotHorizontal, rotVertical);
                }
            }
            else if (!webGLRightClickRotation)
            {
                CameraRotation(cam, rotHorizontal, rotVertical);
            }

            if (character != null)
            {
                character.Move(movement * Time.fixedDeltaTime);
            }
            else
            {
                Debug.LogError("CharacterController is missing.");
            }
        }

        void CameraRotation(GameObject cam, float rotHorizontal, float rotVertical)
        {
            transform.Rotate(0, rotHorizontal * Time.fixedDeltaTime, 0);
            cam.transform.Rotate(-rotVertical * Time.fixedDeltaTime, 0, 0);

            if (Mathf.Abs(cam.transform.localRotation.x) > 0.7)
            {
                float clamped = 0.7f * Mathf.Sign(cam.transform.localRotation.x);
                Quaternion adjustedRotation = new Quaternion(clamped, cam.transform.localRotation.y, cam.transform.localRotation.z, cam.transform.localRotation.w);
                cam.transform.localRotation = adjustedRotation;
            }
        }
    }
}
