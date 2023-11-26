using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using SunTemple; // Assuming CharController_Motor is in the SunTemple namespace

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    public AudioSource musicAudioSource; // Add this line to declare AudioSource
    public AudioSource effectsAudioSource; // Add this line to declare AudioSource
    public Light lightSource; // Reference to the Light component

    [SerializeField] private bool isSavingJson = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SaveGame()
    {
        PlayerData playerData = GetPlayerData();
        AllGameData data = new AllGameData(playerData);
        SaveAllGameData(data);
    }

    public PlayerData GetPlayerData()
    {
        Transform playerTransform = PlayerState.Instance.playBody;

        if (playerTransform != null)
        {
            Vector3 position = playerTransform.position;
            Quaternion rotation = playerTransform.rotation;
            PlayerData playerData = new PlayerData(position, rotation);
            return playerData;
        }
        else
        {
            Debug.LogError("Player's body reference is missing.");
            return null;
        }
    }

    public void SaveAllGameData(AllGameData gameData)
    {
        if (isSavingJson)
        {
            // Implement saving to JSON here
            SaveGameDataToJsonFile(gameData);
        }
        else
        {
            SaveGameDataToBinaryFile(gameData);
        }
    }

    private void SaveGameDataToJsonFile(AllGameData gameData)
    {
        string path = Application.persistentDataPath + "/save_game.json";
        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(path, json);
        Debug.Log("Data saved to " + path);
    }

    private void SaveGameDataToBinaryFile(AllGameData gameData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save_game.bin";

        try
        {
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(stream, gameData);
            }

            Debug.Log("Data saved to " + path);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error while saving data: " + e.Message);
        }
    }

    public AllGameData LoadAllGameData()
    {
        if (isSavingJson)
        {
            // Implement loading from JSON here
            return LoadGameDataFromJsonFile();
        }
        else
        {
            return LoadGameDataFromBinaryFile();
        }
    }

    private AllGameData LoadGameDataFromJsonFile()
    {
        string path = Application.persistentDataPath + "/save_game.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            AllGameData data = JsonUtility.FromJson<AllGameData>(json);
            Debug.Log("Data loaded from " + path);
            return data;
        }
        return null;
    }

    private AllGameData LoadGameDataFromBinaryFile()
    {
        string path = Application.persistentDataPath + "/save_game.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                AllGameData data = formatter.Deserialize(stream) as AllGameData;
                Debug.Log("Data loaded from " + path);
                return data;
            }
        }
        return null;
    }

    // In LoadGame method:
public void LoadGame()
{
    AllGameData gameData = LoadAllGameData();
    if (gameData != null)
    {
        PlayerData playerData = gameData.PlayerData; // Retrieve player data from gameData
        SetPlayerData(playerData);

        // Update the player's position and rotation
        CharController_Motor charControllerMotor = FindObjectOfType<CharController_Motor>();
        charControllerMotor.SetPlayerPositionAndRotation(playerData.position.ToVector3(), playerData.rotation.ToQuaternion());
    }
}





// In SetPlayerData method:
private void SetPlayerData(PlayerData playerData)
{
    if (playerData == null)
    {
        Debug.LogError("PlayerData is null.");
        return;
    }

    Vector3 position = playerData.position.ToVector3();
    Quaternion rotation = playerData.rotation.ToQuaternion();

    // Check if PlayerState.Instance is not null
    if (PlayerState.Instance != null)
    {
        if (PlayerState.Instance.playBody != null)
        {
            Transform playerTransform = PlayerState.Instance.playBody;
            playerTransform.position = position;
            playerTransform.rotation = rotation;
            Debug.Log("Player position set to: " + position);
            Debug.Log("Player rotation set to: " + rotation);
        }
        else
        {
            Debug.LogError("playBody reference in PlayerState is missing.");
        }
    }
    else
    {
        Debug.LogError("PlayerState is missing.");
    }
}

    public void StartLoadedGame()
    {
        SceneManager.LoadScene("lvl1test");
        StartCoroutine(DelayedLoading());
    }

    private IEnumerator DelayedLoading()
    {
        yield return new WaitForSeconds(1f);
        LoadGame();
    }

    // Volume settings section (No changes made)
    [System.Serializable]
    public class VolumeSettings
    {
        public float music;
        public float effects;
    }
 [System.Serializable]
    public class LightSettings
    {
        public float lightIntensity;
    }

    public void SaveVolumeSettings(float _music, float _effects)
    {
        VolumeSettings volumeSettings = new VolumeSettings()
        {
            music = _music,
            effects = _effects,
        };

        string json = JsonUtility.ToJson(volumeSettings);
        PlayerPrefs.SetString("Volume", json);
        PlayerPrefs.Save();
        Debug.Log("Saved to PlayerPrefs");

        // Update the volume of musicAudioSource if it exists
        if (musicAudioSource != null)
        {
            musicAudioSource.volume = _music;
        }
        
        // Update the volume of effectsAudioSource if it exists
        if (effectsAudioSource != null)
        {
            effectsAudioSource.volume = _effects;
        }
    }

  public void SaveLightSettings(float _lightIntensity)
    {
        LightSettings lightSettings = new LightSettings()
        {
            lightIntensity = _lightIntensity
        };

        string json = JsonUtility.ToJson(lightSettings);
        PlayerPrefs.SetString("LightIntensity", json);
        PlayerPrefs.Save();
        Debug.Log("Saved light intensity settings to PlayerPrefs");
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
            return new VolumeSettings();
        }
    }
      public LightSettings LoadLightSettings()
    {
        if (PlayerPrefs.HasKey("LightIntensity"))
        {
            string json = PlayerPrefs.GetString("LightIntensity");
            return JsonUtility.FromJson<LightSettings>(json);
        }
        else
        {
            return new LightSettings();
        }
    }
}
