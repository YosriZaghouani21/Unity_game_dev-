using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }
    public AudioSource musicAudioSource;
    public AudioSource effectsAudioSource;
    public Light lightSource;

    public Button backBTN;
    public Slider lightSlider;
    public TextMeshProUGUI lightValue;

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
        LoadSettings();

        lightSlider.onValueChanged.AddListener(delegate { UpdateLightIntensity(); });
        musicSlider.onValueChanged.AddListener(delegate { UpdateMusicVolume(); });
        effectsSlider.onValueChanged.AddListener(delegate { UpdateEffectsVolume(); });

        backBTN.onClick.AddListener(() =>
        {
            SaveManager.Instance.SaveVolumeSettings(musicSlider.value, effectsSlider.value);
            SaveManager.Instance.SaveLightSettings(lightSlider.value);
        });
    }

    private void LoadSettings()
    {
        SaveManager.VolumeSettings volumeSettings = SaveManager.Instance.LoadVolumeSettings();
        SaveManager.LightSettings lightSettings = SaveManager.Instance.LoadLightSettings();

        lightSlider.value = lightSettings.lightIntensity;
        musicSlider.value = volumeSettings.music;
        effectsSlider.value = volumeSettings.effects;

        UpdateLightIntensity();
        UpdateMusicVolume();
        UpdateEffectsVolume();
    }

    private void UpdateLightIntensity()
    {
        lightValue.text = lightSlider.value.ToString();
        if (lightSource != null)
        {
            lightSource.intensity = lightSlider.value;
        }
    }

    private void UpdateMusicVolume()
    {
        musicValue.text = musicSlider.value.ToString();
        musicAudioSource.volume = musicSlider.value;
    }

    private void UpdateEffectsVolume()
    {
        effectsValue.text = effectsSlider.value.ToString();
        effectsAudioSource.volume = effectsSlider.value;
    }
}
