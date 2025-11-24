using UnityEngine;

public class ActivateShopScreen : MonoBehaviour, Interactable
{
    public GameObject shopScreen;
    void Start()
    {
        shopScreen = GameObject.Find("ShopScreen"); // Finds the shopScreen panel to attach to the script
        shopScreen.SetActive(false); // Can only find gameobjects while they are active, this will turn the screen off and hide them from players.
    }

    public void OnInteraction()
    {
        shopScreen.SetActive(true); // activates the screen and let's it be clickable for players.
        Time.timeScale = 0; // Freezes time, so player can't move and the day does not progress.
    }
    public void UnfreezeTime()
    { // will attach this function to a button to be pressed.
        Time.timeScale = 1f; // unfreezes time
    }

    public string ToolTip()
    {
        return "Press E for the Shop.";

    }
}
