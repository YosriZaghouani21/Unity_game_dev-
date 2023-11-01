using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSaveManager : MonoBehaviour
{
    public static MainMenuSaveManager Instance { get; private set; }

    [System.Serializable]
    public class VolumeSettings
    {
        public float music;
        public float effects;
        public float master;
    }

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

    public void SaveVolumeSettings(float _music, float _effects, float _master)
    {
        VolumeSettings volumeSettings = new VolumeSettings()
        {
            music = _music,
            effects = _effects,
            master = _master
        };

        string json = JsonUtility.ToJson(volumeSettings);
        PlayerPrefs.SetString("Volume", json);
        PlayerPrefs.Save();
        print("Saved to Player Pref");
    }

    public VolumeSettings LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            string json = PlayerPrefs.GetString("Volume");
            return JsonUtility.FromJson<VolumeSettings>(json);
        }
        else
        {
            // If no volume settings are saved, return default values
            return new VolumeSettings();
        }
    }
}
