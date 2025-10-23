using UnityEngine;

public class TestInteract : MonoBehaviour, Interactable // Attach , Interactable to include interact options
{
    public void OnInteraction() // from the Interactable script
    {
        Debug.Log(gameObject.name); // just printing out the attached gameObject
    }

    public string ToolTip() // from the Interactable script
    {
        return "Press E"; // changes the attache
    }
}
