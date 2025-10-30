using System.ComponentModel;
using UnityEngine;

public class Item
{
    // private variables of all item stats
    private int _itemId;
    private string _itemName;
    private string _itemDescription;
    private int _itemValue;
    private Sprite _itemIcon;
    private string _itemMesh;
    private ItemType _itemType;
    // public properties to access stats
    public int ItemId
    {
        get { return _itemId; }
        set { _itemId = value; }
    }
    public string ItemName
    {
        get { return _itemName; }
        set { _itemName = value; }

    }
    public string ItemDescription
    {
        get { return _itemDescription; }
        set { _itemDescription = value; }
    }

    public int Value
    {
        get { return _itemValue; }
        set { _itemValue = value; }
    }
    public Sprite ItemIcon
    {
        get { return _itemIcon; }
        set { _itemIcon = value; }
    }

    public string Mesh
    {
        get { return _itemMesh; }
        set { _itemMesh = value; }
    }
    public ItemType Type
    {
        get { return _itemType; }
        set { _itemType = value; }
    }

}
public enum ItemType
{
    Seed,
    Crop,
    Tool
}