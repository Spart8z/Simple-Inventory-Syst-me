using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Hand hand;

    public Animator AnimInv;

    public int slotSelection = 0;

    public Transform Selector;
    public Image itemTakeUI;

    public Sprite[] itemIcon; // SELECTED ICONS
    public GameObject[] UIInventorySlot; // SLOT POSITIONS
    public GameObject[] Stockage; // ARRAY OF ITEMS (ADD HAND IN FIRST SLOT)


    //public Axe axe; // PREFAB OBJECT

    void Update()
    {
        //******************************INPUTE******************************\\

        for (int i = 1; i <= 9; i++) // INPUTE [KEYBOARD]
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                slotSelection = i - 1; // Adjust index to start from 0
                StartCoroutine("OpenInv");
                Update_Inventory();
                break;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && slotSelection < 8)
        {
            slotSelection += 1;
            StartCoroutine("OpenInv");
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && slotSelection > 0)
        {
            slotSelection -= 1;
            StartCoroutine("OpenInv");
        }
        Selector.position = UIInventorySlot[slotSelection].transform.position; // POSITION THE SELECTOR ON THE ITEM SLOT
        itemTakeUI.sprite = UIInventorySlot[slotSelection].GetComponent<Image>().sprite; // SET THE SELECTED ITEM ICON
    }
    public void Update_Inventory()
    {
        //******************************ACTIVATE AND DEACTIVATE OBJECTS******************************\\

        for (int i = 0; i <= 8; i++)
        {
            if (Stockage[i] != null && i != slotSelection)
            {
                Stockage[i].SetActive(false);
            }
            else if (Stockage[i] != null)
            {
                Stockage[slotSelection].SetActive(true);
            }
            continue;
        }

        //******************************ADD OBJECTS ICON IN THE INVENTORY******************************\\

        for (int i = 1; i <= 8; i++) // i = 1 THE FIRST OBJECT IS HAND
        {
            if (Stockage[i] != null && Stockage[i].tag == "Portable object") // ADD CONDITIONS IF YOU ADD MORE ITEM TYPES
            {
                UIInventorySlot[i].GetComponent<Image>().sprite = itemIcon[0]; // SET THE ICON FOR THE ITEM
            }
            if(Stockage[i] == null)
            {
                UIInventorySlot[i].GetComponent<Image>().sprite = itemIcon[1]; // SET THE ICON FOR THE ITEM
            }
            if (Stockage[i] != null && Stockage[i].gameObject.name == "axe(Clone)")
            {
                UIInventorySlot[i].GetComponent<Image>().sprite = itemIcon[2]; // SET THE ICON FOR THE ITEM
            }
            continue;
        }

        //******************************FIX BUG WITH TAKING OBJECT AND CHANGING SLOT SELECTION******************************\\

        if (slotSelection != 0 && hand.grabbedOB != null)
        {
            hand.dropGameObject();
        }
        //axe.takeAxePerformInInventory();
    }
    public void DropObjectOfInventory()
    {
        Stockage[slotSelection].GetComponent<BoxCollider>().enabled = true;
        Stockage[slotSelection].GetComponent<Rigidbody>().isKinematic = false;
        Stockage[slotSelection].transform.position = hand.objectHolder.position;
        Stockage[slotSelection].gameObject.name = "axe";
        UIInventorySlot[slotSelection].GetComponent<Image>().sprite = null;
        Stockage[slotSelection].transform.parent = null;
        Stockage[slotSelection] = null;
    }
    IEnumerator OpenInv()
    {
        Update_Inventory();
        AnimInv.SetBool("Open", true);
        yield return new WaitForSeconds(5);
        AnimInv.SetBool("Open", false);
    }
}