using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    public Button backBTN;
    public Slider masterSlider;
    public TextMeshProUGUI masterValue;

    public Slider musicSlider;
    public TextMeshProUGUI musicValue;

    public Slider effectsSlider;
    public TextMeshProUGUI effectsValue;

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
        // Load saved volume settings
        LoadVolumeSettings();

        backBTN.onClick.AddListener(() =>
        {
            // Save the updated volume settings when the back button is clicked
            MainMenuSaveManager.Instance.SaveVolumeSettings(musicSlider.value, effectsSlider.value, masterSlider.value);
        });

        StartCoroutine(LoadAndApplySettings()); // Corrected StartCoroutine
    }

    private IEnumerator LoadAndApplySettings()
    {
        LoadAndSetVolume();
        yield return new WaitForSeconds(0.1f); // Corrected WaitForSeconds
    }

    private void LoadAndSetVolume()
    {
        MainMenuSaveManager.VolumeSettings volumeSettings = MainMenuSaveManager.Instance.LoadVolumeSettings();
        masterSlider.value = volumeSettings.master;
        musicSlider.value = volumeSettings.music;
        effectsSlider.value = volumeSettings.effects;
        Debug.Log("Volume Settings are loaded"); // Changed "print" to "Debug.Log"
    }

    private void Update()
    {
        // Update the text values based on slider values
        masterValue.text = masterSlider.value.ToString();
        musicValue.text = musicSlider.value.ToString();
        effectsValue.text = effectsSlider.value.ToString();
    }

    private void LoadVolumeSettings()
    {
        // Load and apply volume settings when the script starts
        MainMenuSaveManager.VolumeSettings volumeSettings = MainMenuSaveManager.Instance.LoadVolumeSettings();
        masterSlider.value = volumeSettings.master;
        musicSlider.value = volumeSettings.music;
        effectsSlider.value = volumeSettings.effects;
    }
}
