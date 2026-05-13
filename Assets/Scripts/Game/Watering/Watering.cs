using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Handles the watering, including emptying the water and updating the water bar UI
/// </summary>
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
    //Reference to the inventory script
    public Inventory inventoryManager;
    //Reference to the image for the water bar
    public Image waterBar;
    //reference to the text for the current water amount
    public Text waterText;
    #endregion
    #region Functions
    /// <summary>
    /// Empties the water from the watering can 
    /// </summary>
     public void EmptyWater()
     {
        //Checks to see if the player is currently trying to skip the time based on time and the water speed variable
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
    /// <summary>
    /// Updates the waterbar UI and text to display how much water the player has
    /// </summary>
    public void UpdateUI()
    {
        //Sets the water bar fill amount to the current water amount divided by max water amount
        //This is because fill amount can only be done in 0-1
        waterBar.fillAmount = currentWaterAmount / maxWaterAmount;
        //Sets the text to say the current water in text form
        waterText.text = $"{(int)currentWaterAmount}% water";
    }
    #endregion
    #region Unity Callbacks
    //Called once per frame
    public void Update()
    {
        //Checks to see if the player is holding the watering can
        if (inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemName == "Watering Can")
        {
            //Calls onb the Empty Water function
            EmptyWater();
        }

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
        UpdateUI();
    }
    //Called on the first frame of the game
    private void Start()
    {
        //retrieves the interaction script through the find game object with tag and get component
        interactionManager = GameObject.FindGameObjectWithTag("InteractBox").GetComponent<Interaction>();
        //retrieves the inventory script through the game game object with tag and get component
        inventoryManager = GameObject.FindGameObjectWithTag("Inventory Manager").GetComponent <Inventory>();
        //Retrieves the image component from the water image object in the Unity Canvas
        waterBar = GameObject.Find("WaterImage").GetComponent<Image>();
        //Retrieves the text component from the water text object in teh Unity canvas
        waterText = GameObject.Find("WaterText").GetComponent<Text>();
    }
    #endregion
}
