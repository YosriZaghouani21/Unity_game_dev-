using UnityEngine;
using UnityEngine.SceneManagement; // Corrected the namespace

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("lvl1test"); // Corrected SceneManager spelling

    }

    public void ExitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
