using UnityEngine;
using SunTemple; // Assuming CharController_Motor is in the SunTemple namespace

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; private set; }
    public Transform playBody; // Reference to the player's body (CharController_Motor transform)

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure the PlayerState persists across scenes
        }
    }

    public void SetPlayBody(Transform bodyTransform)
    {
        playBody = bodyTransform;
    }


    private void Start()
    {
        // You can set the playBody to the CharController_Motor transform here
        CharController_Motor charControllerMotor = FindObjectOfType<CharController_Motor>();
        if (charControllerMotor != null)
        {
            playBody = charControllerMotor.transform;
        }
        else
        {
            Debug.LogWarning("CharController_Motor not found. Make sure it exists in the scene.");
        }
    }
}
