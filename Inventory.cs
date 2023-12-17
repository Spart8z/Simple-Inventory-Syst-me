using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Hand hand;
    public Rigidbody HandRB;

    public Animator AnimInv;
    public Image itemTakeUI;

    public Transform Selector;
    public int slotSelection = 0;

    public GameObject[] InventorySlot; // ARRAY OF ITEMS
    public GameObject[] UIInventorySlot; // SLOT POSITIONS
    public Sprite[] itemIcon; // SELECTED ICONS

    public Axe axe;

    void Update()
    {
        // INPUT HANDLING
        if (Input.GetKeyDown("1"))
        {
            slotSelection = 0;
            StartCoroutine("OpenInv");
        }
        else if (Input.GetKeyDown("2"))
        {
            slotSelection = 1;
            StartCoroutine("OpenInv");
        }
        else if (Input.GetKeyDown("3"))
        {
            slotSelection = 2;
            StartCoroutine("OpenInv");
        }
        else if (Input.GetKeyDown("4"))
        {
            slotSelection = 3;
            StartCoroutine("OpenInv");
        }
        else if (Input.GetKeyDown("5"))
        {
            slotSelection = 4;
            StartCoroutine("OpenInv");
        }
        else if (Input.GetKeyDown("6"))
        {
            slotSelection = 5;
            StartCoroutine("OpenInv");
        }
        else if (Input.GetKeyDown("7"))
        {
            slotSelection = 6;
            StartCoroutine("OpenInv");
        }
        else if (Input.GetKeyDown("8"))
        {
            slotSelection = 7;
            StartCoroutine("OpenInv");
        }
        else if (Input.GetKeyDown("9"))
        {
            slotSelection = 8;
            StartCoroutine("OpenInv");
        }
        else
        {
            Selector.position = UIInventorySlot[slotSelection].transform.position; // POSITION THE SELECTOR ON THE ITEM SLOT
            itemTakeUI.sprite = UIInventorySlot[slotSelection].GetComponent<Image>().sprite; // SET THE SELECTED ITEM ICON
            InventoryUpdate();
            if (Input.GetAxis("Mouse ScrollWheel") > 0f && slotSelection < 8)
            {
                slotSelection += 1;
                StartCoroutine("OpenInv");
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f && slotSelection > 0)
            {
                slotSelection -= 1;
                StartCoroutine("OpenInv");
            }
        }
        if (slotSelection != 0 && HandRB != null) // FIX BUG WITH TAKING OBJECT AND CHANGING SLOT SELECTION
        {
            hand.grabbedRB = null;
            HandRB.isKinematic = false;
            HandRB = null;
        }
    }

    void InventoryUpdate()
    {
        for (int i = 0; i <= 8; i++) // ACTIVATE AND DEACTIVATE OBJECTS
        {
            if (InventorySlot[i] != null && i != slotSelection)
            {
                InventorySlot[i].SetActive(false);
            }
            else if (InventorySlot[i] != null)
            {
                InventorySlot[slotSelection].SetActive(true);
            }
            continue;
        }
        for (int i = 0; i <= 8; i++) // ADD OBJECTS TO THE INVENTORY
        {
            if (InventorySlot[i] != null && InventorySlot[i].tag == "axe") // ADD CONDITIONS IF YOU ADD MORE ITEM TYPES
            {
                UIInventorySlot[i].GetComponent<Image>().sprite = itemIcon[1]; // SET THE ICON FOR THE ITEM
            }
            continue;
        }
    }

    IEnumerator OpenInv()
    {
        axe.takeAxePerformInInventory();
        AnimInv.SetBool("Open", true);
        yield return new WaitForSeconds(5);
        AnimInv.SetBool("Open", false);
    }
}
