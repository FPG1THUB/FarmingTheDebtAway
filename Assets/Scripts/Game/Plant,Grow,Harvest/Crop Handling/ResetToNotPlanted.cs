using UnityEngine;
/// <summary>
/// Interactable script to make it so that when harvesting it will reset the plot and give the player an item
/// </summary>
public class ResetToNotPlanted : MonoBehaviour, Interactable
{
    public int itemID; // stores the itemID of the crop we are harvesting
    public string itemName; // stores the item name of the crop we are harvesting
    public int amount; // stores the amount in which the crop will give us(aka if its set to 1 when we harvest we will get 1 crop)
    public CropHandler cropHandler;//Stores the crop Handler so that we can call on its functions

    public void OnInteraction()
    {
        //Local variable to check if the crop is already in the players inventory
        int check = -1;
        //Fetches the inventory script from the inventory manager
        Inventory inventory = GameObject.FindGameObjectWithTag("Inventory Manager").GetComponent<Inventory>();
        foreach (Item item in inventory.inventory)// checks each slot in the inventory for a dupe.
        {
           if (item.ItemId == itemID) // checks if the item is already in the item slot
           {
              check = 1;//Sets the check to 1, meaning it will say that the item is already in the players inventory
              //increase item
              item.ItemQuantity += amount; 
           }
        }
        if (check != 1)//Checks to see if there is no crop in the player inventory
        {
            //add item
            inventory.inventory.Add(ItemData.CreateItem(itemID));
            int temp = inventory.inventory.Count;//temp variable to store how many items in the inventory
            inventory.inventory[temp - 1].ItemQuantity = amount; //adds the amount to the crops inventory slot
        }
                //Updates the hotbar to display the addition of the crop
                inventory.UpdateHotBarDisplay();
                //Sets the crop to none, therefore setting it back to 
                cropHandler.HarvestCrop();
    }

    public string ToolTip()
    {
        return "Press E to harvest crop";
    }
    
}
