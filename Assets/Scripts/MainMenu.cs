using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button LoadGameBTN;

    private void Start()
    {
        // Add a null check before adding the click event listener.
        if (LoadGameBTN != null)
        {
            LoadGameBTN.onClick.AddListener(() =>
            {
                if (SaveManager.Instance != null)
                {
                    SaveManager.Instance.StartLoadedGame();
                }
                else
                {
                    Debug.LogError("SaveManager Instance is not assigned!");
                }
            });
        }
        else
        {
            Debug.LogError("LoadGameBTN is not assigned!");
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene("lvl1");
    }

    public void ExitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
