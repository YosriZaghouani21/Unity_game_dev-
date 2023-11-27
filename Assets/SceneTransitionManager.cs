using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public Transform playerCharacter; // Assign the player character's transform in the Inspector

    public void LoadNextScene(string sceneName)
    {
        // Set the player body reference in the PlayerState script
        PlayerState.Instance.SetPlayBody(playerCharacter);

        // Load the next scene
        SceneManager.LoadScene(sceneName);
    }
}
