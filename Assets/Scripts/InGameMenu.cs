using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public void BacktoMainMenu()
    {
        SceneManager.LoadScene("mainmenu");
    }
}
