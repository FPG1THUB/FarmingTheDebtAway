using UnityEngine;
using System.Collections.Generic;

public class Watering : MonoBehaviour
{
    #region Variables
    //stores what the current water amount is
    public float currentWaterAmount = 100f;
    //Stores how much water the watering can can hold
    public float maxWaterAmount = 100f;
    //Stores the minimum amount of water the can can hold
    public float minWaterAmount = 0f;
    //Stores the speed in which the watering can empties and refills
    public int waterSpeed = 5;
    //Reference to the interaction script
    public Interaction interactionManager;

    #endregion
    #region Functions
    //This is used to empty the water from the watering can
     public void EmptyWater()
     {
        //Checks to see if the player is currently trying to skip the time
        if (!interactionManager.skip)
        {
            //Checks if the key E was pressed
            if (Input.GetKey(KeyCode.E))
            {
                //Checks to see if the current water amount if greater than the minimum and lower or the same amount as the maximum that the can can hold
                if (currentWaterAmount > minWaterAmount && currentWaterAmount <= maxWaterAmount)
                {
                    //Decreases the amount of water held based off of time and the speed of the water
                    currentWaterAmount -= Time.deltaTime * waterSpeed;
                }
            }
        }

     }
    #endregion
    #region Unity Callbacks
    //Called once per frame
    public void Update()
    {
        //Calls onb the Empty Water function
        EmptyWater();
        //Checks to see if the current water amount has gone past the max amount
        if (currentWaterAmount > maxWaterAmount)
        {
            //If so, set it to max
            currentWaterAmount = maxWaterAmount;
        }
        //Checks to see if the current water amount has gone under the minimum amount
        if (currentWaterAmount < minWaterAmount)
        {
            //Sets teh current water amount to minimum
            currentWaterAmount = minWaterAmount;
        }
    }
    //Called on the first frame of the game
    private void Start()
    {
        //retrieves the interaction script through the find game object wiht tag and get component
        interactionManager = GameObject.FindGameObjectWithTag("InteractBox").GetComponent<Interaction>();
    }
    #endregion
}
