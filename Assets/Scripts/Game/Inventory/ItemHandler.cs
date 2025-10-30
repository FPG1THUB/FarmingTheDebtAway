using UnityEngine;

public class ItemHandler : MonoBehaviour, Interactable
{ // Slap this on an object that we want to be picked up.
    public int itemID; // in the inspector for the item, put the case # for the relevent object
    public string itemName;
    public void OnInteraction()
    {

        Inventory inventory = GameObject.FindGameObjectWithTag("Inventory Manager").GetComponent<Inventory>();
        inventory.inventory.Add(ItemData.CreateItem(itemID));
        inventory.UpdateHotBarDisplay();
        Destroy(gameObject);
    }

    public string ToolTip()
    {
        Debug.Log(ItemData.CreateItem(itemID));
        return "Press E for"+ItemData.CreateItem(itemID);


    }
}
