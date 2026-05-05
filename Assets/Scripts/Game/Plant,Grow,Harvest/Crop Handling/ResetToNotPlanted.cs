using UnityEngine;

public class ResetToNotPlanted : MonoBehaviour, Interactable
{
    public int itemID; // in the inspector for the item, put the case # for the relevent object
    public string itemName; // 
    public int amount; // In the inspector, let's us decide how much of the thing will be added to the inventory at a time.
    public int moneyValue;
    public CropHandler cropHandler;
    int cropExists = 0;

    public void OnInteraction()
    {
        int check = -1;
        Inventory inventory = GameObject.FindGameObjectWithTag("Inventory Manager").GetComponent<Inventory>();
            if (cropExists != 1)
            {
            foreach (Item item in inventory.inventory)// checks each slot in the inventory for a dupe.
            {
                if (item.ItemId == itemID) // checks for items already added to the inventory.
                {
                    check = 1;
                    //increase item
                    item.ItemQuantity += amount; // allows for adding multiples
                    cropExists = 1;
                }
            }
            if (check != 1)
            {
                //add item
                inventory.inventory.Add(ItemData.CreateItem(itemID)); 
                                                                      //and set value to the amount we add 
                int temp = inventory.inventory.Count;
                inventory.inventory[temp - 1].ItemQuantity = amount; // 
                cropExists = 1;

            }
            
                inventory.UpdateHotBarDisplay();
                cropHandler.HarvestCrop();
            }


    }

    public string ToolTip()
    {
        if (cropExists != 1)
        {
        return "Press E to harvest crop";
        }
        else
        {
            return "";
        }
    }
}
