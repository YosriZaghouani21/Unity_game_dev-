using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public float floatSpeed = 1.0f;
    public float floatHeight = 0.5f;
    public float bounceHeight = 0.2f; // Adjust the bounce height
    private Vector3 initialPosition;
   /* private void Start()
    {
        initialPosition = transform.position;
    }*/

   /* private void Update()
    {
        // Calculate the new Y position using a combination of a sine wave and a bounce effect
        float sineWave = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        float bounceEffect = Mathf.Abs(Mathf.Sin(Time.time * floatSpeed * 2)) * bounceHeight; // Adjust the frequency of the bounce

        float newY = initialPosition.y + sineWave + bounceEffect;
        Vector3 newPosition = new Vector3(transform.position.x, newY, transform.position.z);

        // Update the position of the GameObject
        transform.position = newPosition;
    }*/
    private void OnMouseDown()
    {
        // Handle the click event here, e.g., open a menu, play an animation, etc.
        Debug.Log("Clicked on KeyItem!");
    }
}
