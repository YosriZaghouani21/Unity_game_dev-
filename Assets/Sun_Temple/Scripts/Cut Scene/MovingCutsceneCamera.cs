using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCutsceneCamera : MonoBehaviour
{
    public Transform startPoint; // Set these in the Inspector
    public Transform endPoint; // Set these in the Inspector
    public float moveDuration; // Duration of the camera move in seconds
    public float pauseDuration; // Duration of the pause at the end in seconds

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
            if (!gameObject.activeInHierarchy)
            {
                Debug.LogWarning("Camera GameObject has been disabled; coroutine is exiting.");
                yield break; // Exit if the GameObject is disabled
            }

            float t = (Time.time - startTime) / moveDuration;
            transform.position = Vector3.Lerp(startPoint.position, endPoint.position, t);
            transform.rotation = Quaternion.Slerp(startPoint.rotation, endPoint.rotation, t);
            yield return null;
        }

        // Ensure the camera is exactly at the end point
        transform.position = endPoint.position;
        transform.rotation = endPoint.rotation;

        Debug.Log("Reached end point. Now pausing for " + pauseDuration + " seconds.");
        yield return new WaitForSeconds(pauseDuration);
        Debug.Log("Pause complete. Coroutine has finished.");
    }
}
