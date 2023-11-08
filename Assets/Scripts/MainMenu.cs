using UnityEngine;
using UnityEngine.UI; // You need to include the UI namespace for the Button type.
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button LoadGameBTN;

    private void Start()
    {
        // Add a click event listener to the LoadGameBTN button.
         LoadGameBTN.onClick.AddListener(() =>
        {
            SaveManager.Instance.StartLoadedGame();
        });
    }

    public void NewGame()
    {
        SceneManager.LoadScene("lvl1test");
    }

    public void ExitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
