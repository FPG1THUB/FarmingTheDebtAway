using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
/// <summary>
/// This class handles the UI of the inventory. it stores UI and connects and updates it according to newly created items or adding 
/// items, and stores the money and selected hotbars to show what item the player is holding and how much money they have.
/// Additionally, it stores the items in which the player has.
/// Attach this to an inventory manager script, as it does not need to be connected to any assets
/// </summary>
public class Inventory : MonoBehaviour
{
    // Stores the GameObjects that will be used for displaying the selecting, naming, quantities, and the background of each hotbar slot and item
    public GameObject[] hotbarSlots = new GameObject[8]; // The visual slot that the item will fill.
    public GameObject[] hotbarNames = new GameObject[8]; // The text element attached to the slot.(WHY ISNT THIS A TEXT WTF im keeping this here for anyone who reads this this aint mine im just commenting)
    public GameObject[] hotbarAmount = new GameObject[8]; // Text element that tells how many things there are.(AGAIN WHY ISNT THIS A TEXT)
    public GameObject[] hotbarBackground = new GameObject[8]; // The background image elements of the slots, used for the visual selection of a slot.
    [SerializeField]public List<Item> inventory = new List<Item>(8); // Array that holds what the player has gathered.
    public int _selectedHotbarIndex = 0; // Used for the visual selection of a slot.
    public GameObject currencyText; // The text element of the currency section.
    public int money; // A value that can be saved to a save state.
    /*
        ok one thing to note Zachy boi, you do not use GameObject for storing the UI elements, you use it for storing object where you wish to
        manipulate its properties/component. While technically yes this does work, it hurts my soul :( if u wanna store and utilise UI elements
        bloody do Text/Image/ whatever!!!!!!  ik he wont see this but i still put this here
    */

    public void Start()
    {
        ConnectHotBar();
        UpdateHotBarDisplay();
        UpdateCurrency(0);
    }
    /// <summary>
    /// Find all hotbar elements and stores them
    /// </summary>
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
        // Finds and stores the currency counter object. There is only the one currency coutner so it does not need a for loop.
        currencyText = GameObject.Find("Currency Counter");
    }
    /// <summary>
    /// Applies item information and sprites onto the hotbar to be displayed
    /// </summary>
    public void UpdateHotBarDisplay()
    {
        //Goes through each inventory slot
        for (int i = 0; i < inventory.Count; i++)
        { 
            //implements teh sprite, name, and quantity to the hotbar slot
            hotbarSlots[i].GetComponent<Image>().sprite = inventory[i].ItemIcon;
            hotbarNames[i].GetComponent<Text>().text = inventory[i].ItemName; 
            hotbarAmount[i].GetComponent<Text>().text = "x " + inventory[i].ItemQuantity; 
            //Checks if the quantity of an item is equal or less than zero
            if (inventory[i].ItemQuantity <= 0) 
            {
                //Removes the item from the inventory slot
                inventory.Remove(inventory[i]); 
                //Reloads the empty hotbar slots and resets the icon, name and quantity
                hotbarSlots[inventory.Count].GetComponent<Image>().sprite = Resources.Load<Sprite>("Icons/box for inventory"); 
                hotbarNames[inventory.Count].GetComponent<Text>().text = ""; 
                hotbarAmount[inventory.Count].GetComponent<Text>().text = ""; 
                
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
