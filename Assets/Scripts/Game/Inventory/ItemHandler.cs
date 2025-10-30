using UnityEngine;

public class ItemHandler : MonoBehaviour, Interactable
{
    public int itemID;

    public void OnInteraction()
    {

        Inventory inventory = GameObject.FindGameObjectWithTag("Inventory Manager").GetComponent<Inventory>();
        inventory.inventory.Add(ItemData.CreateItem(itemID));
        inventory.UpdateHotBarDisplay();
        Destroy(gameObject);
    }

    public string ToolTip()
    {
        throw new System.NotImplementedException();
    }
}
