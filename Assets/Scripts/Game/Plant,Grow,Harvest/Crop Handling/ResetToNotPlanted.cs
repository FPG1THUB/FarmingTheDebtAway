using UnityEngine;

public class ResetToNotPlanted : MonoBehaviour, Interactable
{
    public CropHandler cropHandler;

    public void OnInteraction()
    {
        cropHandler.HarvestCrop();
    }

    public string ToolTip()
    {
        return "Press E to harvest crop";
    }
}
