using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SunTemple
{
    public class CharController_Motor : MonoBehaviour
    {
        CharacterController character;
        float gravity = -9.8f;

        void Start()
        {
            character = GetComponent<CharacterController>();
            SpawnOnGround();
        }

        void FixedUpdate()
        {
            ApplyGravity();
        }

        void ApplyGravity()
        {
            Vector3 gravityVector = new Vector3(0, gravity, 0);
            character.Move(gravityVector * Time.fixedDeltaTime);
        }

        void SpawnOnGround()
        {
            // Set the player's position to be on the ground
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                Vector3 spawnPosition = hit.point;
                character.Move(spawnPosition - transform.position);
            }
        }
    }
}