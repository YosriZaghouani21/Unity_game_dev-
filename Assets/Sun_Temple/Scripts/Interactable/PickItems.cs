using UnityEngine;

public class PickItems : MonoBehaviour
{
    public GameObject Item;
    public Transform ItemParent;
    private Vector3 initialBookPosition;
    private Quaternion initialBookRotation;
    private bool isPickedUp = false;
    public bool playerInRange;
    // Start is called before the first frame update
    void Start()
    {

        Item.GetComponent<Rigidbody>().isKinematic = true;
        initialBookRotation = Item.transform.rotation;
        initialBookPosition = Item.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.F))
        {
            DropBook();
        }
    }

    private void DropBook()
    {
        ItemParent.DetachChildren();
        Item.transform.position = initialBookPosition;
        Item.transform.rotation = initialBookRotation;
        Item.GetComponent<Rigidbody>().isKinematic = false;
        Item.GetComponent<MeshCollider>().enabled = true;
        isPickedUp = false;
        playerInRange = false;
    }
    void EquipBook()
    {
        Item.GetComponent<Rigidbody>().isKinematic = true;
        Item.transform.position = ItemParent.transform.position;
        Item.transform.rotation = ItemParent.transform.rotation;
        Item.GetComponent<MeshCollider>().enabled = false;
        Item.transform.SetParent(ItemParent);
        isPickedUp = true;
        Item.transform.Rotate(84.939f, -1.999f, -184.178f);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                EquipBook();
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
