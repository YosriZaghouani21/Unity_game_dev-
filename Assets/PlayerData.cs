using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public SerializableVector3 position;
    public SerializableQuaternion rotation;

    public PlayerData(Vector3 _position, Quaternion _rotation)
    {
        position = new SerializableVector3(_position);
        rotation = new SerializableQuaternion(_rotation);
    }
}



