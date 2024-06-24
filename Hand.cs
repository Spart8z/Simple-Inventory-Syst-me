using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour // THE SCRIPT TAKE OBJECT AND ADD TO INVENTORY 
{
    [SerializeField] Camera cam;

    [SerializeField] int maxGrabDistance;

    [SerializeField] Transform objectHolder;
    [SerializeField] Transform Player;


    [SerializeField] Inventory inventory;
    [SerializeField] Transform TakeObjectPos;

    public Rigidbody grabbedRB;

    public GameObject grabbedOB;
    public GameObject SimpleAxePref; // ADD GAMEOBJECT IF YOU WANT ADD ITEM

    void Update()
    {
        if (grabbedRB)
        {
            if (Input.GetKeyDown("x")) // CHANGE KEY FOR OPEN THE INVENTORY BOTH IN THE INVENTORY SCRIPT
            {
                for(int i = 0; i < 9; i++)
                {
                    if (inventory.UIInventorySlot[i] == null && i != 0) // SLOT 1 ONLY NULL
                    {
                        
                        if(grabbedOB.tag == "axe") // IF YOU WANT ADD OBJECT ADD IF HER AND CHANGE TAG WITH YOU ITEM TAG
                        {
                            inventory.UIInventorySlot[i] = Instantiate(SimpleAxePref, TakeObjectPos.parent); // TakeObjectPos IS THE POSITION TO SPAWN ITEM IN THE INVENTORY
                            inventory.UIInventorySlot[i].tag = "axe";
                        }
                        inventory.Update_Inventory();
                        Destroy(grabbedOB);
                        break;
                    }
                }
            }
            grabbedOB.transform.parent = objectHolder.transform;
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
                            inventory.HandRB = grabbedRB;
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
        grabbedOB = null;
        grabbedRB.isKinematic = false;
        grabbedRB = null;
    }
}