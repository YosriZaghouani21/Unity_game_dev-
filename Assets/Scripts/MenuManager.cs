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
    public GameObject settingMenu;

    public bool isMenuOpen;
    private int selectedOption; // Track the selected menu option

    private CursorLockMode previousCursorLockMode;

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
        selectedOption = 0; // Initialize the selected option
        UpdateMenuSelection();
    }

    private void Update()
    {
        if (isMenuOpen)
        {
            HandleMenuNavigation();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                OpenMenu();
            }
        }
    }

    void HandleMenuNavigation()
    {
        // Detect keyboard input for menu navigation
        float menuInput = Input.GetAxis("Vertical");

        if (menuInput > 0.1f)
        {
            // Move selection up
            selectedOption--;
            if (selectedOption < 0)
                selectedOption = 2; // Adjust the number of menu options

            UpdateMenuSelection();
        }
        else if (menuInput < -0.1f)
        {
            // Move selection down
            selectedOption++;
            if (selectedOption > 2) // Adjust the number of menu options
                selectedOption = 0;

            UpdateMenuSelection();
        }

        // Handle menu option selection
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectMenuOption();
        }
    }

    void UpdateMenuSelection()
    {
        // Handle visual feedback for the selected menu option
        menu.SetActive(selectedOption == 0);
        saveMenu.SetActive(selectedOption == 1);
        settingMenu.SetActive(selectedOption == 2);
    }

    void SelectMenuOption()
    {
        // Implement menu option selection logic here
        switch (selectedOption)
        {
            case 0:
                // Start the game (e.g., load a new scene)
                SceneManager.LoadScene("YourGameSceneName");
                break;
            case 1:
                // Open or close the save menu
                saveMenu.SetActive(!saveMenu.activeSelf);
                break;
            case 2:
                // Open or close the settings menu
                settingMenu.SetActive(!settingMenu.activeSelf);
                break;
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
}
