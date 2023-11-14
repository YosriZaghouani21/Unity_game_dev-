using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCutsceneCamera : MonoBehaviour
{
    public Transform startPoint;  // Set these in the Inspector
    public Transform endPoint;    // Set these in the Inspector
    public float moveDuration = 5f; // Duration of the camera move in seconds

    // Call this method to start the camera movement
    public void StartCameraMove()
    {
        StartCoroutine(MoveCamera());
    }

    private IEnumerator MoveCamera()
    {
        float timeElapsed = 0;

        while (timeElapsed < moveDuration)
        {
            // Calculate the interpolation factor
            float t = timeElapsed / moveDuration;

            // Interpolate the position and rotation between the start and end points
            transform.position = Vector3.Lerp(startPoint.position, endPoint.position, t);
            transform.rotation = Quaternion.Slerp(startPoint.rotation, endPoint.rotation, t);

            // Increment the time elapsed
            timeElapsed += Time.deltaTime;

            // Wait until the next frame
            yield return null;
        }

        // Ensure the final position and rotation are exactly at the end point
        transform.position = endPoint.position;
        transform.rotation = endPoint.rotation;
    }
}