using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Hand hand;
    public Rigidbody HandRB;

    public Animator AnimInv;

    public GameObject[] InventorySlot; // ARRAY OF ITEMS (ADD HAND IN FIRST SLOT)

    public int slotSelection = 0;

    public Transform Selector;
    public Image itemTakeUI;
        public Sprite[] itemIcon; // SELECTED ICONS
    public GameObject[] UIInventorySlot; // SLOT POSITIONS

    public Axe axe; // PREFAB OBJECT

    void Update()
    {
        for (int i = 1; i <= 9; i++) // INPUTE [BY CHATGPT]
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
            }else if (Input.GetAxis("Mouse ScrollWheel") < 0f && slotSelection > 0)
            {
                slotSelection -= 1;
                StartCoroutine("OpenInv");
            }
            Selector.position = UIInventorySlot[slotSelection].transform.position; // POSITION THE SELECTOR ON THE ITEM SLOT
            itemTakeUI.sprite = UIInventorySlot[slotSelection].GetComponent<Image>().sprite; // SET THE SELECTED ITEM ICON
    }
    public void Update_Inventory()
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
        if (slotSelection != 0 && HandRB != null) // FIX BUG WITH TAKING OBJECT AND CHANGING SLOT SELECTION
        {
            hand.grabbedOB.transform.parent = null;
            hand.grabbedRB = null;
            HandRB.isKinematic = false;
            HandRB = null;
        }
        axe.takeAxePerformInInventory();
    }
    IEnumerator OpenInv()
    {
        Update_Inventory();
        AnimInv.SetBool("Open", true);
        yield return new WaitForSeconds(5);
        AnimInv.SetBool("Open", false);
    }
}
