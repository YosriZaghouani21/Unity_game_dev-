using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCutsceneCamera : MonoBehaviour
{
    public Transform startPoint; // Set these in the Inspector
    public Transform endPoint; // Set these in the Inspector
    public float moveDuration; // Duration of the camera move in seconds
    public float pauseDuration; // Duration of the pause at the end in seconds

    // Call this method to start the camera movement
    public void StartCameraMove()
    {
        Debug.Log("Camera move started.");
        StartCoroutine(MoveCamera());
    }

     IEnumerator MoveCamera()
    {
        float startTime = Time.time;
        float endTime = startTime + moveDuration;

        // Move the camera to the end point over the duration.
        while (Time.time <= endTime)
        {
            float t = (Time.time - startTime) / moveDuration;
            transform.position = Vector3.Lerp(startPoint.position, endPoint.position, t);
            transform.rotation = Quaternion.Slerp(startPoint.rotation, endPoint.rotation, t);
            yield return null;
        }

        // Ensure the camera is exactly at the end point
        transform.position = endPoint.position;
        transform.rotation = endPoint.rotation;
        Debug.Log("Camera move completed. Now pausing.");

        // Pause at the end point for the specified duration.
        yield return new WaitForSeconds(pauseDuration);

        Debug.Log("Pause complete. Coroutine has finished.");
    }

    IEnumerator test()
    {

        Debug.Log("AAAA");

        // Pause at the end point for the specified duration.
        yield return new WaitForSeconds(pauseDuration);

        Debug.Log("BBB");
    }
}