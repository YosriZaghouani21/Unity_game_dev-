using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneEnter : MonoBehaviour
{
    public GameObject thePlayer;
    public GameObject cutsceneCam;
    private MovingCutsceneCamera movingCutsceneCamera; // Reference to the moving camera script

    void Start()
    {
        // Get the MovingCutsceneCamera component from the cutscene camera
        movingCutsceneCamera = cutsceneCam.GetComponent<MovingCutsceneCamera>();
        if (movingCutsceneCamera == null)
        {
            Debug.LogError("MovingCutsceneCamera script not found on the cutscene camera!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        cutsceneCam.SetActive(true);
        thePlayer.SetActive(false);

        // Start the camera movement
        if (movingCutsceneCamera != null)
        {
            movingCutsceneCamera.StartCameraMove();
        }

        StartCoroutine(FinishCut());
    }

    IEnumerator FinishCut()
    {
        yield return new WaitForSeconds(5);
        thePlayer.SetActive(true);
        cutsceneCam.SetActive(false);
    }
}