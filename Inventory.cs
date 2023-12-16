using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Hand hand;
    public Rigidbody HandRB;

    public GameObject InventoryUI;
    public Image itemTakeUI;
    public Transform Selector;

    public int slotSelection = 0;

    public GameObject[] InventorySlot;

    public GameObject[] UIInventorySlot;

    public Sprite[] itemIcon;

    void Update()
    {
        //INPUTE 
        if (Input.GetButtonDown("Fire3")) // OPEN INVENTORY
        {
            InventoryUI.SetActive(!InventoryUI.activeSelf);
        }

        if (Input.GetKeyDown("1"))
        {
            slotSelection = 0;
        }
        else if (Input.GetKeyDown("2"))
        {
            slotSelection = 1;
        }
        else if (Input.GetKeyDown("3"))
        {
            slotSelection = 2;
        }
        else if (Input.GetKeyDown("4"))
        {
            slotSelection = 3;
        }
        else if (Input.GetKeyDown("5"))
        {
            slotSelection = 4;
        }
        else if (Input.GetKeyDown("6"))
        {
            slotSelection = 5;
        }
        else if (Input.GetKeyDown("7"))
        {
            slotSelection = 6;
        }
        else if (Input.GetKeyDown("8"))
        {
            slotSelection = 7;
        }
        else if (Input.GetKeyDown("9"))
        {
            slotSelection = 8;
        }
        else
        {
            Selector.position = UIInventorySlot[slotSelection].transform.position; //PRINT THE SLECTORE ON ITEM SLOT 
            itemTakeUI.sprite = UIInventorySlot[slotSelection].GetComponent<Image>().sprite; //PRINT THE SLECTORE ON ITEM SLOT 
            InventoryUpdate();
        }
        if (slotSelection != 0 && HandRB != null) // FIX BUG WITH THE TAKE OBJECT AND CHANGE SLOT SLECTIONNE
        {
            hand.grabbedRB = null;
            HandRB.isKinematic = false;
            HandRB = null;
        }
    }
    void InventoryUpdate()
    {
        for (int i = 0; i <= 8; i++) // ACTIVE AND DISABLE OBJECT 
        {
            if (InventorySlot[i] != null && i != slotSelection)
            {
                InventorySlot[i].SetActive(false);
            }
            else
            {
                InventorySlot[slotSelection].SetActive(true);
            }
            continue;
        }
        for (int i = 0; i <= 8; i++) // ADD OBJECT IN TO THE INVENTORY 
        {
            if (InventorySlot[i] != null && InventorySlot[i].tag == "axe") // ADD IF IF YOU ADD ITEM
            {
                UIInventorySlot[i].GetComponent<Image>().sprite = itemIcon[1]; // SELECT YOUR ICON IN ITEM ICON 
            }
            continue;
        }
    }
}
