using UnityEngine;

public class ItemHandler : MonoBehaviour, Interactable
{ // Slap this on an object that we want to be picked up.
    public int itemID; // in the inspector for the item, put the case # for the relevent object
    public string itemName; // 
    public int amount; // In the inspector, let's us decide how much of the thing will be added to the inventory at a time.
    public void OnInteraction()
    {
        int check = -1;
        Inventory inventory = GameObject.FindGameObjectWithTag("Inventory Manager").GetComponent<Inventory>();
        foreach (Item item in inventory.inventory)
        {
            if (item.ItemId == itemID)
            {
                check = 1;
                //increase item
                item.ItemQuantity += amount;                
            }
        }
        if (check != 1)
        {
            //add item
            inventory.inventory.Add(ItemData.CreateItem(itemID)); //
            //and set value to the amount we add 
           int temp = inventory.inventory.Count; 
            inventory.inventory[temp-1].ItemQuantity = amount; // finds that 

        }

        inventory.UpdateHotBarDisplay();
        Destroy(gameObject);
    }

    public string ToolTip()
    {
        Debug.Log(ItemData.CreateItem(itemID));
        return "Press E for"+ItemData.CreateItem(itemID);


    }
}
