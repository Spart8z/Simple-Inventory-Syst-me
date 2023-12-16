using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{

    [SerializeField] Camera cam;
    [SerializeField] float maxGrabDistance = 10f;

    [SerializeField] Transform objectHolder;

    [SerializeField] Transform Player;

    [SerializeField] Inventory inventory;

    [SerializeField] Transform HandT;

    public Rigidbody grabbedRB;
    GameObject grabbedOB;

    public GameObject SimpleAxePref;




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
            if (Input.GetKeyDown("e"))
            {
                for(int i = 0; i < 9; i++)
                {
                    if (inventory.InventorySlot[i] == null)
                    {
                        
                        if(grabbedOB.tag == "axe")
                        {
                            inventory.InventorySlot[i] = Instantiate(SimpleAxePref, HandT);
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