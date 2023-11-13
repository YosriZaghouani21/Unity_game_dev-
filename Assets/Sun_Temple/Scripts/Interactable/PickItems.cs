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

    void Start()
    {
        Item.GetComponent<Rigidbody>().isKinematic = true;
        initialBookRotation = Item.transform.rotation;
        initialBookPosition = Item.transform.position;
        inventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
    }

    void Update()
    {
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
            inventoryManager.AddItem(Item.tag); // Add the key to the inventory
            isKeyEquipped = true;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
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