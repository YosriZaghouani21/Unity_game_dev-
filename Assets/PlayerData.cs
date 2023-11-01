using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData 
{
public float[] PlayerStats;
public float[] PlayerPositionAndRotation;
//public string[] inventoryContent;
public PlayerData(float[] _playerStats, float[] _playerPosAndRot){
    PlayerStats = _playerStats;
    PlayerPositionAndRotation = _playerPosAndRot;
}
}
