using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class Inventory : MonoBehaviour
{
    // Various gameobjects that need to be found.
    public GameObject[] hotbarSlots = new GameObject[8]; // The visual slot that the item will fill.
    public GameObject[] hotbarNames = new GameObject[8]; // The text element attached to the slot.
    public GameObject[] hotbarAmount = new GameObject[8]; // Text element that tells how many things there are.
    public GameObject[] hotbarBackground = new GameObject[8]; // The background image elements of the slots, used for the visual selection of a slot.
    public List<Item> inventory = new List<Item>(); // Array that holds what the player has gathered.
    public int _selectedHotbarIndex = 0; // Used for the visual selection of a slot.
    public GameObject currencyText; // The text element of the currency section.
    public int money; // A value that can be saved to a save state.


    public void Start()
    {
        // connect our hotbar slots to the UI elements
        ConnectHotBar();
        // if there is save data, then this will update the hotbar with that data.
        UpdateHotBarDisplay();
        UpdateCurrency(0);
    }

    void ConnectHotBar()
    {
        // Each for loop is finding each of the GameObject slots and then connecting them to the relevent variables.
        for (int i = 0; i < hotbarSlots.Length; i++)
        { 
            hotbarSlots[i] = GameObject.Find("Slot_" + i + "_Image");
        }
        for (int i = 0; i < hotbarNames.Length; i++)
        {
            hotbarNames[i] = GameObject.Find("Slot_" + i + "_Text");
        }
        for (int i = 0; i < hotbarAmount.Length; i++)
        {
            hotbarAmount[i] = GameObject.Find("Slot_" + i + "_Amount");
        }
        for (int i = 0; i < hotbarBackground.Length; i++)
        {
            hotbarBackground[i] = GameObject.Find("Slot_" + i + "_BackgroundImage");
        }
        // There is only the one currency coutner so it does not need a for loop.
        currencyText = GameObject.Find("Currency Counter");
    }
    public void UpdateHotBarDisplay()
    {
        // Mainly to be used in other scripts when an item is added or removed from the inventory.
        for (int i = 0; i < inventory.Count; i++)
        { // Using inventory.Count since that's where the item data is stored.
            hotbarSlots[i].GetComponent<Image>().sprite = inventory[i].ItemIcon; // applies the sprite of the item onto the slot.
            hotbarNames[i].GetComponent<Text>().text = inventory[i].ItemName; // applies the name of the item onto the slot.
            hotbarAmount[i].GetComponent<Text>().text = "x " + inventory[i].ItemQuantity; // updates the text with the amount of items in that slot.
            if (inventory[i].ItemQuantity <= 0) // If the amount of an item a player has is equal to or less then zero, then it needs to be removed from the inventory.
            {
                inventory.Remove(inventory[i]); // Clears out the inventory slot.
                // Due to how removing an item works, it will move the item 'down' a slot and leave a duplicate behind, which needs to be cleared out.
                hotbarSlots[inventory.Count].GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/box for inventory"); // Applies the background image to the slot to give it an appearance of being empty.
                hotbarNames[inventory.Count].GetComponent<Text>().text = ""; // Removes the name of the object.
                hotbarAmount[inventory.Count].GetComponent<Text>().text = ""; // Removes the amount of the item.

                if ((inventory.Count != 0) && i != inventory.Count) // this checks to make sure that the object being deleted isn't the only thing in the inventory, and isn't the item in the last slot.
                { // If the item is by itself when removed, it creates a null error, if it's not the last slot, then it doesn't update with the new items statistics.
                    // puts the item that is now in the slot into the visual.

                    hotbarSlots[i].GetComponent<Image>().sprite = inventory[i].ItemIcon;
                    hotbarNames[i].GetComponent<Text>().text = inventory[i].ItemName;
                    hotbarAmount[i].GetComponent<Text>().text = "x " + inventory[i].ItemQuantity;

                }
            }
        }
    }
    public void UpdateCurrency(int amount) // To update currency when buying or selling something.
    {
        if ((amount + money) >= 0) // Checks to make sure that the amount being subtracted to the money won't bring you below 0.
        {
            money += amount; // adds the value to the money variable, to be stored when saving.
            currencyText.GetComponent<Text>().text = "$ " + (money); // Then updates the currency tracker accordingly.
        }
    }

    private void Update()
    {
        SelectingHotbarSlot(); // so that the player can quickly switch hotbar slots at a time.
    }

    private void SelectingHotbarSlot()
    {
        hotbarBackground[_selectedHotbarIndex].GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/box for inventory"); // Resets the highlighted box to the normal box.
        // this will detect if a player presses any of the numbered keys.
        if (Input.GetKeyDown("1"))
        {
            _selectedHotbarIndex = 0;

        }
        else if (Input.GetKeyDown("2"))
        {
            _selectedHotbarIndex = 1;

        }
        else if (Input.GetKeyDown("3"))
        {
            _selectedHotbarIndex = 2;

        }
        else if (Input.GetKeyDown("4"))
        {
            _selectedHotbarIndex = 3;

        }
        else if (Input.GetKeyDown("5"))
        {
            _selectedHotbarIndex = 4;

        }
        else if (Input.GetKeyDown("6"))
        {
            _selectedHotbarIndex = 5;

        }
        else if (Input.GetKeyDown("7"))
        {
            _selectedHotbarIndex = 6;

        }
        else if (Input.GetKeyDown("8"))
        {
            _selectedHotbarIndex = 7;

        }
        // Then shows which slot is selected by changing the background box to a brighter version.
        hotbarBackground[_selectedHotbarIndex].GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/box highlighted");
    }

    //public void UseItem()
    //{
    //    //if (inventory[_selectedHotbarIndex].ItemName == "Hoe")
    //    //{

    //    //}
    //}
}
