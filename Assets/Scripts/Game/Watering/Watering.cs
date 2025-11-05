using UnityEngine;
using System.Collections.Generic;

public class Watering : MonoBehaviour, Interactable 
{
    #region Variables
    //stores what the current water amount is
    public float currentWaterAmount = 100f;
    //Stores how much water the watering can can hold
    public float maxWaterAmount = 100f;
    //Stores the minimum amount of water the can can hold
    public float minWaterAmount = 0f;
    public int waterSpeed = 5;
    public Interactable interactedObject;
    #endregion
    #region Functions
     public void EmptyWater()
     {
        if (Input.GetKey(KeyCode.E))
        {
            if(currentWaterAmount > minWaterAmount && currentWaterAmount <= maxWaterAmount)
            {
                currentWaterAmount -= Time.deltaTime * waterSpeed;
            }


        }
     }
    public void FillWater()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if(GameObject.FindGameObjectWithTag("Tap"))
            {
                OnInteraction();
            }
        }
    }
    #endregion
    #region Unity Callbacks
    public void Update()
    {

        EmptyWater();
        FillWater();
        if(currentWaterAmount > maxWaterAmount)
        {
            currentWaterAmount = maxWaterAmount;
        }
    }

    public void OnInteraction()
    {
       if(GameObject.FindGameObjectWithTag("Tap"))
        {
           currentWaterAmount += Time.deltaTime * waterSpeed;
        }
    }

    public string ToolTip()
    {
        return "HI";
    }
    #endregion
}
