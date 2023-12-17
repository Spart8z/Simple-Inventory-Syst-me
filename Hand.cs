using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour // THE SCRIPT TAKE OBJECT AND ADD TO INVENTORY 
{

    [SerializeField] Camera cam;
    [SerializeField] float maxGrabDistance;

    [SerializeField] Transform objectHolder;

    [SerializeField] Transform Player;

    [SerializeField] Inventory inventory;

    [SerializeField] Transform HandT;

    public Rigidbody grabbedRB;
    GameObject grabbedOB;

    public GameObject SimpleAxePref; // ADD GAMEOBJECT IF YOU WANT ADD ITEM

    void Update()
    {
        if (grabbedRB)
        {
            if (Input.GetKeyDown("x")) // CHANGE KEY FOR OPEN THE INVENTORY BOTH IN THE INVENTORY SCRIPT
            {
                for(int i = 0; i < 9; i++)
                {
                    if (inventory.InventorySlot[i] == null)
                    {
                        
                        if(grabbedOB.tag == "axe") // IF YOU WANT ADD OBJECT ADD IF HER AND CHANGE TAG WITH YOU ITEM TAG
                        {
                            inventory.InventorySlot[i] = Instantiate(SimpleAxePref, HandT); // HANDT IS THE POSITION TO SPAWN ITEM IN THE INVENTORY
                            inventory.InventorySlot[i].tag = "axe";
                        }
                        Destroy(grabbedOB);
                        break;
                    }
                }
            }
            grabbedOB.transform.parent = objectHolder.transform;
        }
        if (grabbedOB != null && Input.GetMouseButtonDown(1))
        {
            dropGameObject();
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (grabbedRB)
            {
                dropGameObject();
            }
            else
            {
                RaycastHit hit;
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                if (Physics.Raycast(ray, out hit, maxGrabDistance))
                {
                    grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                    grabbedOB = hit.collider.gameObject;
                    if (grabbedRB)
                    {
                        grabbedRB.isKinematic = true;
                        inventory.HandRB = grabbedRB;
                    } 
                }
            }
        }
    }
    void dropGameObject()
    {
        grabbedOB.transform.parent = null;
        grabbedRB.isKinematic = false;
        grabbedRB = null;
    }
}