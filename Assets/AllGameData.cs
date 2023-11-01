using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AllGameData
{
    public PlayerData PlayerData; // Corrected property name to "PlayerData"

    public AllGameData(PlayerData playerData)
    {
        PlayerData = playerData;
    }
}
