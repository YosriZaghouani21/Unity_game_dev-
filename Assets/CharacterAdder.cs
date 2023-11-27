using UnityEngine;

public class PlayerCharacterScaler : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // Reference to the player transform
    [SerializeField] private Transform characterPrefabTransform; // Reference to the character prefab transform
    [SerializeField] private float distanceFromPlayer = 2.0f; // Distance in front of the player
    [SerializeField] private float yOffset = -0.5f; // Offset to move character prefab lower
    [SerializeField] private float characterScaleFactor = 0.5f; // Adjust this factor to resize the character

    private void Start()
    {
        if (playerTransform == null || characterPrefabTransform == null)
        {
            Debug.LogError("Player or character prefab is not assigned!");
            return;
        }

        AdjustCharacterToPlayer();
    }

    private void Update()
    {
        // Continually adjust character scale and position while the game is running
        if (playerTransform != null && characterPrefabTransform != null)
        {
            AdjustCharacterToPlayer();
        }
    }

    private void AdjustCharacterToPlayer()
    {
        Vector3 playerSize = GetSizeWithChildren(playerTransform.gameObject);
        Vector3 characterSize = GetSizeWithChildren(characterPrefabTransform.gameObject);

        if (!IsValidSize(playerSize) || !IsValidSize(characterSize))
        {
            Debug.LogWarning("Invalid size detected!");
            return;
        }

        Vector3 scale = CalculateScale(playerSize, characterSize);
        SetCharacterScale(characterPrefabTransform.gameObject, scale);

        Vector3 playerForward = playerTransform.forward;
        Vector3 characterOffset = playerForward * distanceFromPlayer + Vector3.up * yOffset;

        characterPrefabTransform.position = playerTransform.position + characterOffset;
    }

    private bool IsValidSize(Vector3 size)
    {
        return !float.IsInfinity(size.x) && !float.IsInfinity(size.y) && !float.IsInfinity(size.z) &&
               !float.IsNaN(size.x) && !float.IsNaN(size.y) && !float.IsNaN(size.z) &&
               size.x != 0 && size.y != 0 && size.z != 0;
    }

    private Vector3 GetSizeWithChildren(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

        foreach (Renderer renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }

        return bounds.size;
    }

    private Vector3 CalculateScale(Vector3 playerSize, Vector3 characterSize)
    {
        return new Vector3(
            playerSize.x / Mathf.Max(characterSize.x, 0.001f) * characterScaleFactor,
            playerSize.y / Mathf.Max(characterSize.y, 0.001f) * characterScaleFactor,
            playerSize.z / Mathf.Max(characterSize.z, 0.001f) * characterScaleFactor
        );
    }

    private void SetCharacterScale(GameObject character, Vector3 scale)
    {
        character.transform.localScale = scale;
    }
}
