using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour // THE SCRIPT TAKE OBJECT AND ADD TO INVENTORY 
{

    [SerializeField] Camera cam;
    [SerializeField] float maxGrabDistance = 10f;

    [SerializeField] Transform objectHolder;

    [SerializeField] Transform Player;

    [SerializeField] Inventory inventory;

    [SerializeField] Transform HandT;

    public Rigidbody grabbedRB;
    GameObject grabbedOB;

    public GameObject SimpleAxePref; // ADD GAMEOBJECT IF YOU WANT ADD ITEM




    void FixedUpdate()
    {
        if (grabbedRB)
        {
            grabbedOB.transform.position = objectHolder.position;
            grabbedOB.transform.rotation = Player.rotation;


            if (Input.GetMouseButtonDown(1))
            {
                grabbedRB.isKinematic = false;
                grabbedRB = null;
            }
            if (Input.GetKeyDown("e")) // CHANGE KEY FOR OPEN THE INVENTORY BOTH IN THE INVENTORY SCRIPT
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
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (grabbedRB)
            {
                grabbedRB.isKinematic = false;
                grabbedRB = null;
            }
            else
            {
                RaycastHit hit;
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                if (Physics.Raycast(ray, out hit, maxGrabDistance))
                {
                    grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                    grabbedOB = hit.collider.gameObject;
                    inventory.HandRB = grabbedRB;
                    if (grabbedRB)
                    {
                        grabbedRB.isKinematic = true;
                    }
                }
            }
        }
    }
}