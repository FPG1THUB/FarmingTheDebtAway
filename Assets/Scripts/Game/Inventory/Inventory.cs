using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class Inventory : MonoBehaviour
{
    public GameObject[] hotbarSlots = new GameObject[8];
    public GameObject[] hotbarNames = new GameObject[8];
    public GameObject[] hotbarAmount = new GameObject[8];
    public List<Item> inventory = new List<Item>();
    public int _selectedHotbarIndex = 0;
    public GameObject currencyText;
    public int money;


    public void Start()
    {
        // connect our hotbar slots to the UI elements
        ConnectHotBar();

        UpdateHotBarDisplay();
        UpdateCurrency(0);
    }

    void ConnectHotBar()
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            hotbarSlots[i] = GameObject.Find("Slot_"+i+"_Image");
        }
        for (int i = 0; i < hotbarNames.Length; i++)
        {
            hotbarNames[i] = GameObject.Find("Slot_" + i+"_Text");
        }
        for (int i = 0; i < hotbarAmount.Length; i++)
        {
            hotbarAmount[i] = GameObject.Find("Slot_" + i + "_Amount");
        }
        currencyText = GameObject.Find("Currency Counter");
    }
    public void UpdateHotBarDisplay()
    {
        for (int i = 0; i < inventory.Count; i++)
        {

            hotbarSlots[i].GetComponent<Image>().sprite = inventory[i].ItemIcon;
            hotbarNames[i].GetComponent<Text>().text = inventory[i].ItemName;
            hotbarAmount[i].GetComponent<Text>().text = "x " + inventory[i].ItemQuantity;
            if (inventory[i].ItemQuantity <= 0)
            {
                inventory.Remove(inventory[i]); // Clears out the inventory slot.
            }
        }
    }
    public void UpdateCurrency(int amount)
    {
        if ((amount + money) >= 0)
        {
            money += amount;
            currencyText.GetComponent<Text>().text = "$ " + (money); 
        }
    }

    private void Update()
    {
        SelectingHotbarSlot();
       // Debug.Log(inventory[_selectedHotbarIndex]);
    }
    
    private void SelectingHotbarSlot()
    {
        if (Input.GetKeyDown("1"))
        {
            _selectedHotbarIndex = 0;
            Debug.Log(_selectedHotbarIndex);

        }
        else if (Input.GetKeyDown("2"))
        {
            _selectedHotbarIndex = 1;
            Debug.Log(_selectedHotbarIndex);

        }
        else if (Input.GetKeyDown("3"))
        {
            _selectedHotbarIndex = 2;
            Debug.Log(_selectedHotbarIndex);

        }
        else if (Input.GetKeyDown("4"))
        {
            _selectedHotbarIndex = 3;
            Debug.Log(_selectedHotbarIndex);

        }
        else if (Input.GetKeyDown("5"))
        {
            _selectedHotbarIndex = 4;
            Debug.Log(_selectedHotbarIndex);

        }
        else if (Input.GetKeyDown("6"))
        {
            _selectedHotbarIndex = 5;
            Debug.Log(_selectedHotbarIndex);

        }
        else if (Input.GetKeyDown("7"))
        {
            _selectedHotbarIndex = 6;
            Debug.Log(_selectedHotbarIndex);

        }
        else if (Input.GetKeyDown("8"))
        {
            _selectedHotbarIndex = 7;
            Debug.Log(_selectedHotbarIndex);

        }
    }
    public void UseItem()
    {
        //if (inventory[_selectedHotbarIndex].ItemName == "Hoe")
        //{

        //}
    }

    #region My attempt
    /*
        [Serializable]
        public struct InventoryData // Holds the information for each of the inventory slotws.
        {
            public string inventoryName; // Name of the inventory Slot, going from 0-5 atm.
            public Image inventorySprite; // The image within the inventory slot
            public string inventoryID; // The item currently being stored there.

        }
        [Header("Inventory")]
        [SerializeField] InventoryData[] _inventoryData; // creates a viewable array in te inspector to mess around with.

        [SerializeField] GameObject _selectedSlot; // will use this for the selected slot and image in total.
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }*/
    #endregion


} 
