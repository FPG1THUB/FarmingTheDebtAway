using UnityEngine;

public class ItemHandler : MonoBehaviour, Interactable
{ // Slap this on an object that we want to be picked up.
    public int itemID; // in the inspector for the item, put the case # for the relevent object
    public string itemName; // 
    public int amount; // In the inspector, let's us decide how much of the thing will be added to the inventory at a time.
    public int moneyValue;
    public void OnInteraction()
    {
        
        int check = -1;
        Inventory inventory = GameObject.FindGameObjectWithTag("Manager").GetComponent<Inventory>();
        if ((inventory.money + moneyValue) >= 0) // this will check to see if the resulting interaction would leave the player in the negative.
        {
            inventory.UpdateCurrency(moneyValue); // will update the GUI currency if needed

            foreach (Item item in inventory.inventory)  // checks each slot in the inventory for a dupe.
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
                inventory.inventory.Add(ItemData.CreateItem(itemID)); 
                                                                      //and set value to the amount we add 
                int temp = inventory.inventory.Count;
                inventory.inventory[temp - 1].ItemQuantity = amount; // 

            }

            inventory.UpdateHotBarDisplay();
            Destroy(gameObject);
            
        }
    }

    public string ToolTip() // don't worry about it for now
    {
        Debug.Log("");
        return "Press E to Harvest";
    }
}
