using UnityEngine;
using SunTemple;

public class PlayBodyInitializer : MonoBehaviour
{
    [SerializeField]
    private Transform playBody; // Assign your CharController_Motor object here

    private void Start()
    {
        PlayerState.Instance.SetPlayBody(playBody);
    }
}
