using UnityEngine;

public class PickItems : MonoBehaviour
{
    public GameObject Item;
    public Transform ItemParent;
    private Vector3 initialBookPosition;
    private Quaternion initialBookRotation;
    private bool isPickedUp = false;
    public bool playerInRange;
    public bool isKeyEquipped = false; 
    public InventoryManager inventoryManager;
    public float xRotationThreshold = 0.5f;
    void Start()
    {
        Item.GetComponent<Rigidbody>().isKinematic = true;
        initialBookRotation = Item.transform.rotation;
        initialBookPosition = Item.transform.position;
        inventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
        else
        {
            Debug.LogWarning("Gyroscope not supported on this device");
        }
    }

    void Update()
    {
        if (SystemInfo.supportsGyroscope && playerInRange)
        {
            if (Mathf.Abs(Input.gyro.rotationRateUnbiased.x) > xRotationThreshold)
            {
                if (!isPickedUp)
                {
                    Collider playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();
                    if (playerCollider != null)
                    {
                        EquipItem(playerCollider);
                    }
                }
            }
        }

        if (Input.GetKey(KeyCode.F))
        {
            DropItem();
        }
    }

    public void DropItem()
    {
        ItemParent.DetachChildren();
        Item.transform.position = initialBookPosition;
        Item.transform.rotation = initialBookRotation;
        Item.GetComponent<Rigidbody>().isKinematic = false;
        Item.GetComponent<MeshCollider>().enabled = true;
        isPickedUp = false;
        playerInRange = false;
        if (Item.CompareTag("Key"))
        {
            inventoryManager.RemoveItem(Item.tag); // Remove the key from the inventory
            isKeyEquipped = false;
        }
    }
    void EquipItem(Collider other)
    {
        Item.GetComponent<Rigidbody>().isKinematic = true;
        Item.transform.position = ItemParent.transform.position;
        Item.transform.rotation = ItemParent.transform.rotation;
        Item.GetComponent<MeshCollider>().enabled = false;
        Item.transform.SetParent(ItemParent);
        isPickedUp = true;
        Item.transform.Rotate(84.939f, -1.999f, -184.178f);
        if (Item.CompareTag("Key"))
        {
            inventoryManager.AddItem(this.Item.tag, this);
            isKeyEquipped = true;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Mathf.Abs(Input.gyro.rotationRateUnbiased.x) > xRotationThreshold)
            {
                EquipItem(other);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
   
}