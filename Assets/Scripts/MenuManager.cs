using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    public GameObject menuCanvas;
    public GameObject uiCanvas;
    public GameObject menu;
    public GameObject saveMenu;

    private bool isMenuOpen;
    private int selectedOption;
    private CursorLockMode previousCursorLockMode;

    [SerializeField]
    private KeyCode menuToggleKey = KeyCode.M;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        selectedOption = 0;
        UpdateMenuSelection();
        CloseMenu(); // Start with the menu closed
    }

    private void Update()
    {
        if (Input.GetKeyDown(menuToggleKey))
        {
            ToggleMenu();
        }

        if (isMenuOpen)
        {
            HandleMenuNavigation();
        }
    }

    void HandleMenuNavigation()
    {
        float menuInput = Input.GetAxis("Vertical");

        if (menuInput > 0.1f)
        {
            selectedOption--;
            if (selectedOption < 0)
                selectedOption = 1;
        }
        else if (menuInput < -0.1f)
        {
            selectedOption++;
            if (selectedOption > 1)
                selectedOption = 0;
        }

        UpdateMenuSelection();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectMenuOption();
        }
    }

    void UpdateMenuSelection()
    {
        menu.SetActive(selectedOption == 0);
        saveMenu.SetActive(selectedOption == 1);
    }

    void SelectMenuOption()
    {
        switch (selectedOption)
        {
            case 0:
                StartGame();
                break;
            case 1:
                ToggleSaveMenu();
                break;
        }
    }

    void StartGame()
    {
        SceneManager.LoadScene("YourGameSceneName");
    }

    void ToggleMenu()
    {
        if (isMenuOpen)
        {
            CloseMenu();
        }
        else
        {
            OpenMenu();
        }
    }

    void OpenMenu()
    {
        previousCursorLockMode = Cursor.lockState;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        uiCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        isMenuOpen = true;
    }

    void CloseMenu()
    {
        Cursor.lockState = previousCursorLockMode;
        Cursor.visible = false;

        uiCanvas.SetActive(true);
        menuCanvas.SetActive(false);
        isMenuOpen = false;
    }

    void ToggleSaveMenu()
    {
        saveMenu.SetActive(!saveMenu.activeSelf);
    }

    public void TempSaveGame()
    {
        SaveManager.Instance.SaveGame();
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
