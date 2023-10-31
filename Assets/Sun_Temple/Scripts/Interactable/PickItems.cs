using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickItems : MonoBehaviour
{
    public GameObject Book;
    public Transform BookParent;
    private Vector3 initialBookPosition;
    private Quaternion initialBookRotation;
    private bool isPickedUp = false;
    public bool playerInRange;
    public string ItemName;
    // Start is called before the first frame update
    void Start()
    {

        Book.GetComponent<Rigidbody>().isKinematic = true;
        initialBookRotation = Book.transform.rotation;
        initialBookPosition = Book.transform.position;
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
        BookParent.DetachChildren();
        Book.transform.position = initialBookPosition;
        Book.transform.rotation = initialBookRotation;
        Book.GetComponent<Rigidbody>().isKinematic = false;
        Book.GetComponent<MeshCollider>().enabled = true;
        isPickedUp = false;
        playerInRange = false;
    }
    void EquipBook()
    {
        Book.GetComponent<Rigidbody>().isKinematic = true;
        Book.transform.position = BookParent.transform.position;
        Book.transform.rotation = BookParent.transform.rotation;
        Book.GetComponent<MeshCollider>().enabled = false;
        Book.transform.SetParent(BookParent);
        isPickedUp = true;
        Book.transform.Rotate(84.939f, -1.999f, -184.178f);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                playerInRange = true;
                EquipBook();
            }
        }
    }

}