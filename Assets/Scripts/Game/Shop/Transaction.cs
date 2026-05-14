using UnityEngine;
using UnityEngine.UI;

public class Transaction : MonoBehaviour
{
    [SerializeField]
    public int itemID; // in the inspector for the item, put the case # for the relevent object
    public string itemName; // purely for inspector, doesn't affect anything.
    public int amount; // In the inspector, let's us decide how much of the thing will be added to the inventory at a time.
    // Do not make amount negative.
    public int moneyValue; // Gives us the option of changing how much something is worth in currency.
    Inventory whatPlayerHas; // Will be the variable to hold the players inventory array.
    Watering waterPlayerUpgrade; // Will be used to upgrade the watering can's storage.
    public Text priceText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        whatPlayerHas = GameObject.FindGameObjectWithTag("Inventory Manager").GetComponent<Inventory>();
        waterPlayerUpgrade = GameObject.FindGameObjectWithTag("Manager").GetComponent<Watering>();
        UpdatePriceUI();
    }
    // Made the Buying and Selling of items into seperate transactions due to the necessary checks and balances to avoid infinite money/item glitches.
    public void SellItem()
    {
        foreach (Item item in whatPlayerHas.inventory) // Will loop equal to the number of Items in the array, will grab data from the Item class and verify that the person actually has the item.
        {
            if (item.ItemId == itemID) // Checks the player's inventory for a matching ItemId to what is being sold.
            {
                item.ItemQuantity -= amount; // Subtracts the item from the player's inventory.
                whatPlayerHas.UpdateCurrency(moneyValue); // Then updates the player's currency tracker and money.
            }
        }
        whatPlayerHas.UpdateHotBarDisplay(); // Have to update the HotBar to display the loss of the item after the foreach loop in order to avoid the error of manipulating an array during a foreach loop.
        UpdatePriceUI();
    }

    public void BuyItem()
    {
        if (whatPlayerHas.money >= moneyValue) // checks to make sure that the player has enough currency to actually buy the item.
        {
            whatPlayerHas.UpdateCurrency(-moneyValue); // Then subtracts money from the player's currency.
            int check = -1;
            foreach (Item item in whatPlayerHas.inventory)  // checks each slot in the inventory for a dupe.
            {
                if (item.ItemId == itemID) // checks for items already added to the inventory.
                {
                    check = 1;
                    //increase item
                    item.ItemQuantity += amount; // allows for adding multiples via using amount.
                    
                }
            }
            if (check != 1)
            {
                //add item to inventory array
                whatPlayerHas.inventory.Add(ItemData.CreateItem(itemID));
                //and set value to the amount we add 
                int temp = whatPlayerHas.inventory.Count; // is required in case base ItemQuantity is different to amount.
                whatPlayerHas.inventory[temp - 1].ItemQuantity = amount; // Updates the recently added item with the correct ItemQuantity.                 
            }
            whatPlayerHas.UpdateHotBarDisplay();
            UpdatePriceUI();
        }
    }
    public void UpgradingWateringCan()
    {
        if (whatPlayerHas.money >= moneyValue) // Checks the player has enough money to make the upgrade.
        {
            whatPlayerHas.UpdateCurrency(-moneyValue);
            foreach (Item item in whatPlayerHas.inventory) // Checks for the watering can in the inventory.
            {
                if (item.ItemId == itemID) // the watering can in question.
                {
                    waterPlayerUpgrade.maxWaterAmount += amount; // upgrades the watering can's storage equal to the amount of money spent.
                    waterPlayerUpgrade.waterSpeed += amount; // Upgrades refill speed equal to amount of  money spent.
                    moneyValue += amount; // Adds the amount to the money value to make it more expensive, can be changed if desired.
                }

            }
            UpdatePriceUI();
        }
    }
    public void UpdatePriceUI()
    {
        priceText.text = $"${moneyValue}";
    }
}



