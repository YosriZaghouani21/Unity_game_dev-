using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

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
        DontDestroyOnLoad(gameObject);
    }
public bool isSavingJson;
   #region General Section
  public void SaveGame()
{
    PlayerData playerData = GetPlayerData();
    AllGameData data = new AllGameData(playerData);
    SaveAllGameData(data);
}


    private PlayerData GetPlayerData()
    {
        float[] playerStats = new float[3];
        // You should define the properties you are accessing here, e.g., PlayerState.Instance.currentHeath
        playerStats[0] = 0.0f;
        playerStats[1] = 0.0f;
        playerStats[2] = 0.0f;

        float[] playerPosAndRot = new float[6];
        // You should define the properties you are accessing here, e.g., PlayerState.Instance.playBody.transform.position.x
        playerPosAndRot[0] = 0.0f;
        playerPosAndRot[1] = 0.0f;
        playerPosAndRot[2] = 0.0f;
        playerPosAndRot[3] = 0.0f;
        playerPosAndRot[4] = 0.0f;
        playerPosAndRot[5] = 0.0f;

        return new PlayerData(playerStats, playerPosAndRot);
    }

    public void SaveAllGameData(AllGameData gameData)
    {
        if (isSavingJson)
        {
            // Implement saving to JSON here
        }
        else
        {
            SaveGameDataToBinaryFile(gameData);
        }
    }
    #endregion

    #region Binary Save/Load Section
    public void SaveGameDataToBinaryFile(AllGameData gameData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save_game.bin";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, gameData);
        stream.Close();
        Debug.Log("Data saved to " + path);
    }

    public AllGameData LoadGameDataFromBinaryFile()
    {
        string path = Application.persistentDataPath + "/save_game.bin";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            AllGameData data = formatter.Deserialize(stream) as AllGameData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
        #endregion

#region || --------Settings Section---------- || 
#region || --------Volume Section---------- || 

    [System.Serializable]
    public class VolumeSettings
    {
        public float music;
        public float effects;
        public float master;
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
    #endregion
        #endregion


}
