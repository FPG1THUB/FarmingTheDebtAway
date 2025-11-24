using UnityEngine;

public class ActivateShopScreen : MonoBehaviour, Interactable // Using the interactable class to make it an interactable object.
{
    public GameObject shopScreen; //Is the GameObject 
    void Start()
    {
        shopScreen = GameObject.Find("ShopScreen"); // Finds the shop screen canvas and attaches it to the shopScreen gameobject variable.
        shopScreen.SetActive(false); // Then turns it off, it can only be found if it's active at the start of the scene unfortunately but can be deactivated immediately on being found.
    }

    public void OnInteraction()
    {
        shopScreen.SetActive(true); // Activates the shop screen UI
        Time.timeScale = 0; // Freezes the game, so character can't move and time doesn't progress.
    }
    public void UnfreezeTime() // Necessary function so that the game will continue when the player leaves the shop.
    { // Attached to exit shop button.
        Time.timeScale = 1f; // Normal time speed.
    }

    public string ToolTip()
    {
        return "Press "+(Input.GetKey(KeybindManager.keys["Interact"]))+ "for the Shop.";

    }
}
