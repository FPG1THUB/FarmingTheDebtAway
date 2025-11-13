using UnityEngine;

public class ActivateShopScreen : MonoBehaviour, Interactable
{
    public GameObject shopScreen;
    void Start()
    {
        shopScreen = GameObject.Find("ShopScreen");
        shopScreen.SetActive(false);
    }

    public void OnInteraction()
    {
        shopScreen.SetActive(true);
        Time.timeScale = 0;
    }
    public void UnfreezeTime()
    {
        Time.timeScale = 1f;
    }

    public string ToolTip()
    {
        return "Press E for the Shop.";

    }
}
