using UnityEngine;

public class ActivateShopScreen : MonoBehaviour, Interactable
{
    GameObject shopScreen;
    public void OnInteraction()
    {
        shopScreen = GameObject.Find("ShopScreen");
        shopScreen.SetActive(true);
        
    }

    public string ToolTip()
    {
        return "Press E for the Shop.";

    }
}
