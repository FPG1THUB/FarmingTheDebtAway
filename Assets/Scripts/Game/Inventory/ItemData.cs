using JetBrains.Annotations;
using UnityEngine;

public static class ItemData
{
    public static Item CreateItem(int itemID) //using item.CreateItem(in this box put the case #)
    {
        Item newItem = new Item();
        string _itemName = "";
        string _itemDescription = "";
        int _itemValue = 0; // Can use this for currency values
        string _itemIcon = "";
        string _itemMesh = "";
        ItemType _itemType = ItemType.Seed;
        int _itemQuantity = 0; // default value added to the inventory

        // Assign values based on ItemID
        switch (itemID)
        {
            
            #region Seeds
            case 0:
                _itemName = "Carrot Seed";
                _itemDescription = "A seed for planting carrots.";
                _itemValue = 5;
                _itemIcon = "Icons/carrot_seed_icon";
                _itemMesh = "Meshes/carrot_seed_mesh";
                _itemType = ItemType.Seed;
                _itemQuantity = 1;
                break;
            case 1:
                _itemName = "Potato Seed";
                _itemDescription = "A seed for planting potatoes.";
                _itemValue = 10;
                _itemIcon = "Icons/potato_seed_icon";
                _itemMesh = "Meshes/potato_seed_mesh";
                _itemType = ItemType.Seed;
                _itemQuantity = 1;
                break;
            case 2:
                _itemName = "Tomato Seed";
                _itemDescription = "A seed for planting tomatoes.";
                _itemValue = 15;
                _itemIcon = "Icons/tomato_seed_icon";
                _itemMesh = "Meshes/tomato_seed_mesh";
                _itemType = ItemType.Seed;
                _itemQuantity = 1;
                break;
            #endregion 
            #region Crops
            case 100:
                _itemName = "Carrot";
                _itemDescription = "An orange carrot.";
                _itemValue = 20;
                _itemIcon = "Icons/carrot_icon";
                _itemMesh = "Meshes/carrot_mesh";
                _itemType = ItemType.Crop;
                _itemQuantity = 1;
                break;
            case 101:
                _itemName = "Potato";
                _itemDescription = "An brown potato.";
                _itemValue = 25;
                _itemIcon = "Icons/potato_icon";
                _itemMesh = "Meshes/potato_mesh";
                _itemType = ItemType.Crop;
                _itemQuantity = 1;
                break;
            case 102:
                _itemName = "Tomato";
                _itemDescription = "An red tomato.";
                _itemValue = 30;
                _itemIcon = "Icons/tomato_icon";
                _itemMesh = "Meshes/tomato_mesh";
                _itemType = ItemType.Crop;
                _itemQuantity = 1;
                break;
            #endregion
            #region Tools
            case 200:
                _itemName = "Hoe";
                _itemDescription = "An sturdy hoe.";
                _itemValue = 35;
                _itemIcon = "Icons/hoe_icon";
                _itemMesh = "Meshes/hoe_mesh";
                _itemType = ItemType.Tool;
                _itemQuantity = 1;
                break;
            case 201:
                _itemName = "Watering Can";
                _itemDescription = "An almost full watering can.";
                _itemValue = 40;
                _itemIcon = "Icons/watering_can_icon";
                _itemMesh = "Meshes/watering_can_mesh";
                _itemType = ItemType.Tool;
                _itemQuantity = 1;
                break;
            #endregion
            default:
                Debug.LogWarning("ItemId not recognized: " + itemID);
                break;
        }
        // this will use the functions in Item to overwrite the itemdata.
        newItem.ItemId = itemID;
        newItem.ItemName = _itemName;
        newItem.ItemDescription = _itemDescription;
        newItem.Value = _itemValue;
        newItem.ItemIcon = Resources.Load<Sprite>(_itemIcon);
        newItem.Mesh = _itemMesh;
        newItem.Type = _itemType;
        newItem.ItemQuantity = _itemQuantity;

        return newItem;
    }
}
