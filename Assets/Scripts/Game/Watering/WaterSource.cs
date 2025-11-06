using UnityEngine;

public class WaterSource : MonoBehaviour,Interactable
{
    //reference to the watering script
    public Watering wateringManager;

    private void Start()
    {
        //Retrieves the manager gameobject within the scene and retrieves teh watering script within it
        wateringManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Watering>();
    }
    //Triggers when object collides 
    public void OnInteraction()
    {
        //increases water by time and water speed
        wateringManager.currentWaterAmount += Time.deltaTime * wateringManager.waterSpeed;
        Debug.Log("Refilling");
    }

    public string ToolTip()
    {
        //Shows text telling the player this
        return ("Hold R to refill Watering Can");
    }

  
}
