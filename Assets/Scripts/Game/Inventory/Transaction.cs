using UnityEngine;

public class Transaction : MonoBehaviour
{
    [SerializeField]
    public int itemID; // in the inspector for the item, put the case # for the relevent object
    public string itemName; // 
    public int amount; // In the inspector, let's us decide how much of the thing will be added to the inventory at a time.
    public int moneyValue;
    Inventory whatPlayerHas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        whatPlayerHas = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }
    public void SellItem()
    {
        foreach (Item item in whatPlayerHas.inventory)
        {
            if (item.ItemId == itemID)
            {
                item.ItemQuantity -=amount;
                whatPlayerHas.UpdateCurrency(moneyValue);
            }
        }
    }

    public void BuyItem()
    {
        if (whatPlayerHas.money >= moneyValue)
        {
            int check = -1;
            foreach (Item item in whatPlayerHas.inventory)  // checks each slot in the inventory for a dupe.
            {
                if (item.ItemId == itemID) // checks for items already added to the inventory.
                {
                    check = 1;
                    //increase item
                    item.ItemQuantity += amount; // allows for adding multiples
                }
            }
            if (check != 1)
            {
                //add item
                whatPlayerHas.inventory.Add(ItemData.CreateItem(itemID));
                //and set value to the amount we add 
                int temp = whatPlayerHas.inventory.Count;
                whatPlayerHas.inventory[temp - 1].ItemQuantity = amount; // 
            }
        }
    }


}
