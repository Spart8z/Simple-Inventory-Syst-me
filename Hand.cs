using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hand : MonoBehaviour // THE SCRIPT TAKE OBJECT AND ADD TO INVENTORY 
{

    [SerializeField] Camera cam;
    [SerializeField] int maxGrabDistance;

    public Transform objectHolder;
    [SerializeField] Transform TakeObjectPos;
    [SerializeField] Transform Player;

    [SerializeField] Inventory inventory;

    public Rigidbody grabbedRB;
    public GameObject grabbedOB;

    void Update()
    {
        if (grabbedRB)
        {
            if (Input.GetKeyDown("x")) // CHANGE KEY FOR OPEN THE INVENTORY BOTH IN THE INVENTORY SCRIPT
            {
                for (int i = 0; i < 9; i++)
                {
                    if (inventory.Stockage[i] == null && i != 0) // SLOT 1 ONLY NULL
                    {

                        if (grabbedOB.tag == "Portable object")
                        {
                            inventory.Stockage[i] = Instantiate(grabbedOB, TakeObjectPos); // TakeObjectPos IS THE POSITION TO SPAWN ITEM IN THE INVENTORY
                            inventory.Stockage[i].tag = "Portable object";
                            inventory.Stockage[i].GetComponent<BoxCollider>().enabled = false;
                            Destroy(grabbedOB);
                        }
                        inventory.Update_Inventory();
                        break;
                    }
                }
            }
            if(grabbedOB != null)
            {
                grabbedOB.transform.parent = objectHolder.transform;
            }
        }
        else
        {
            if (Input.GetKeyDown("x"))
            {
                inventory.DropObjectOfInventory();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (grabbedRB != null)
            {
                dropGameObject();
            }
            else
            {
                RaycastHit hit;
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                if (Physics.Raycast(ray, out hit, maxGrabDistance))
                {
                    if (!hit.collider.CompareTag("Untagged"))
                    {
                        grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                        grabbedOB = hit.collider.gameObject;
                        if (grabbedRB)
                        {
                            grabbedRB.isKinematic = true;
                        }
                    }
                    else
                    {
                        Debug.Log("Not catchable");
                    }
                }
            }
        }
    }
    public void dropGameObject()
    {
        grabbedOB.transform.parent = null;
        grabbedRB.isKinematic = false;
        grabbedRB = null;
        grabbedOB = null;
    }
}