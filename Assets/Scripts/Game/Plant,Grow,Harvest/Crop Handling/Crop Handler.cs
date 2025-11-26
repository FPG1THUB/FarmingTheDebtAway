using UnityEngine;

public class CropHandler : MonoBehaviour
{
    #region Variables
    [Space(10), Header("Managers")]
    //Reference to the inventory script
    private Inventory inventoryManager;
    //Reference to the time manager script
    private TimeManager timeManager;
    //Plot handler reference, which will retrieve the script from the plot Gameobject variable above
    public PlotHandler plotHandler;
    //GameObject Reference to the plot, which will be used to retrieve the plot handler within the prefab
    public GameObject plot;

    [Space(10), Header("GameObjects and arrays")]

    //Array to store the potato, tomato, and carrot objects
    public GameObject[] availableCrops = new GameObject[4];
    //public GameObject array to store the different carrot states
    public GameObject[] carrotStagesObjects = new GameObject[4];
    //public gameObject array to store the different potato states
    public GameObject[] potatoStagesObjects = new GameObject[4];
    //public GameObject array to store the different tomato states
    public GameObject[] tomatoStagesObjects = new GameObject[4];

    [Space(10), Header("Planting, growing and harvesting variables")]
    //int to store the growth progress of growth so that the day of planting can reference when the crop should progress to next stage
    public int[] growthProgress = new int[3];
    //Local variable of the one on time manager to track the day it takes to progress to the next stage. This is mostly for debugging purposes
    public int progressByDay;

    [Space(10), Header("Enums")]
    //enum to store the current growth state
    public GrowthState growthState;
    //enum to store the currentStoredCrop
    public Crops currentCrop;


    #endregion

    #region Unity Callbacks
    //Called on the first frame
    private void Start()
    {
        //Retrieves the plot handler script from the plot game object
        plotHandler = plot.GetComponent<PlotHandler>();
        //retrieves the time script from the manager object
        timeManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<TimeManager>();
        //retrieves the inventory script from the inventory script
        inventoryManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Inventory>();
        //Loops through each element available within the crops array
        for (int i = 0; i < availableCrops.Length; i++)
        {
            //retrieves the available crops
            availableCrops[i] = this.transform.GetChild(i).gameObject;

        }
        //Loops through each element available within the carrot states array
        for (int i = 0; i < carrotStagesObjects.Length; i++)
        {
            //Retrieves each state of the crop through the crops array
            carrotStagesObjects[i] = availableCrops[0].transform.GetChild(i).gameObject;
        }
        //Loops through each element available within the potato states array
        for (int i = 0; i < potatoStagesObjects.Length; i++)
        {
            //retrieves each state of the crop through the crops array
            potatoStagesObjects[i] = availableCrops[1].transform.GetChild(i).gameObject;
        }
        //Loops through each element available within the tomato states array
        for (int i = 0; i < tomatoStagesObjects.Length; i++)
        {
            //retrieves each state of the crop through the crops array
            tomatoStagesObjects[i] = availableCrops[2].transform.GetChild(i).gameObject;
        }
        
       

    }
    public void Update()
    {
        CalculateDayProgress();
        SwitchStates();
        GrowCrops();
    }
    #endregion
    #region Switching states
    //function to switch the current crop based on what the current crop is, and the state of the current crop
    public void SwitchStates()
    {
        //int for the current state of the current crop
        int currentState = 0;
        //int for the current crop that is planted
        int currentSeed = 0;
        //Creates an array of the crop stages
        GameObject[] cropArray = new GameObject[4];
        //Sets up a switch statement to go through the current crop
        switch (currentCrop)
        {
            //The first situation is if the plot does not contain any of the crops
            case Crops.None:
                //Sets the current seed to 3, which in the scene is none
                currentSeed = 3;
                //Sets the cropArray to 0, which is none
                cropArray = new GameObject[0];
                break;
                //The second situation is if the plot contains carrots
            case Crops.carrot:
                //sets the current seed to 0, which is carrots
                currentSeed = 0;
                //Sets the crop array to store the different stages of the carrots
                cropArray = carrotStagesObjects;
                break;
                //The third situation is fi the plot contains potatoes
            case Crops.potato:
                //Sets the current seed to 1, which is potatoes
                currentSeed = 1;
                //Sets the cropArray to potato stages to store the different stages of the potatoes
                cropArray = potatoStagesObjects;
                break;
                //The fourth sitation is if the plot contains tomatoes
            case Crops.tomato:
                //sets the current seed to 2, which is tomatoes
                currentSeed = 2;
                //sets the cropArray to tomatoes to store the different tomato stages
                cropArray = tomatoStagesObjects;
                break;
            default:
                //By default, the stored seed is none
                currentSeed = 3;
                cropArray = new GameObject[0];
                break;
        }
      
        //Goes through a switch statements to store the current state of the crop
        switch (growthState)
        {
            //The first situation is if the crop is in planted stage
            case GrowthState.planted:
                //sets the current state to 0, which is planted
                currentState = 0;
                break;
                //The second situation is fi the crop is in baby form
            case GrowthState.baby:
                //Sets the current state to 1, which is baby
                currentState = 1;
                break;
                //The third situation is if the crop is in teen form
            case GrowthState.teen:
                //sets the current state to 2, which is teen
                currentState = 2;
                break;
                //the fourth situation is if the corp is in adult form
            case GrowthState.adult:
                //sets the current state to adult
                currentState = 3;
                break;
            default:
                //By default, the growth state is planted
                currentState = 0;
                break;
        }


        //Goes through each crop type within the available crops array
        foreach (var cropType in availableCrops)
        {
            //for each of them, set them all inactive in the scene
            cropType.SetActive(false);
        }
        //sets teh currently active seed active in the scene
         availableCrops[currentSeed].SetActive(true);
        //checks to see if there is something in teh cropArray
        if (cropArray.Length > 0)
        {
            //If so, loop through each stage within the crop array
            foreach (var cropStage in cropArray)
            {
                //sets them all inactive in the scene
                cropStage.SetActive(false);
            }
        
            //Sets the current state of the crop active in the scene
            cropArray[currentState].SetActive(true);
        }
    }
    #endregion
    #region Planting Crops
    //Function that will check to see if the selected plot does not contain any crop and is dry or wet, and if not, then it will place a crop down depending on what seed the player holds
    public void PlantCrops()
    {
        //Checks to see if the plot is not set to not prepped
        if (plotHandler.plotStates != PlotStates.NotPrepped)
        {
            //Checks to see if there is no crop on the plot
            if (currentCrop == Crops.None)
            {
                //Checks to see if the currently selected seed is a carrot seed and if the player has enough seeds to plant it
                if (inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemName == "Carrot Seed" 
                    && inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemQuantity >= 1)
                {
                    //reduces the carrot seed quantity by 1
                    inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemQuantity--;
                    //Logs how many seeds the player has
                    Debug.Log($"Current Amount of Carrot Seeds:{inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemQuantity}");
                    //Sets the progress by day to 0, which will be used to progress the crop to different stages
                    timeManager.progressByDay = 0;
                    //Sets the current crop to carrot
                    currentCrop = Crops.carrot;
                    //sets the state of the crop to planted
                    growthState = GrowthState.planted;

                }
                //if the above is false, checks to see if the player is trying to plan a potato seed and if the player has enough seeds to plant it
                else if (inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemName == "Potato Seed"
                    && inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemQuantity >= 1)
                {
                    //removes the potato seed by 1 in the inventory
                    inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemQuantity--;
                    //Logs how many seeds the player has
                    Debug.Log($"Current Amount of Potato Seeds:{inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemQuantity}");
                    //sets the current crop to potato
                    currentCrop = Crops.potato;
                    //Sets the current growth state to planted
                    growthState = GrowthState.planted;
                    //Sets the progress by day to 0, which will be used to progress the crop to different stages
                    timeManager.progressByDay = 0;

                }
                //If the above is false, checks to see if the player has tomato seeds selected and has enough to plant them
                else if (inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemName == "Tomato Seed" 
                    && inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemQuantity >= 1)
                {
                    //reduces the tomato seed quantity by 1
                    inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemQuantity--;
                    //Logs how many seeds the player has
                    Debug.Log($"Current Amount of Tomato Seeds:{inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemQuantity}");
                    //Sets the current crop on the plot
                    currentCrop = Crops.tomato;
                    //Sets teh current growth state to planted
                    growthState = GrowthState.planted;
                    //Sets the progress by day to 0, which will be used to progress the crop to different stages
                   timeManager.progressByDay = 0;

                }
                //of everything above is false, do this
                else
                {
                    //Displays on the console that the player is not trying to plant anything right now when interacting with the plot
                    Debug.Log("You are not trying to plant anything right now!");
                }
            }
            else
            {
                //If there is a crop planted in the plot, tell the dev what the crop is and what growth state it is at
                Debug.Log($"currently planted crop:{currentCrop}, current growth stage: {growthState}");
            }
            inventoryManager.UpdateHotBarDisplay();
        }
        else
        {
            //If the plot is not prepped, display a message asking the dev to till the plot to plant seeds on it.
            Debug.Log("You cannot plant anything right! Till the plot so you can plant seeds on it");
        }

    }
    #endregion
    #region Growing Crops
    //Function to take the day in which the crop was planted, and how long it will take to progress to the next stage depending on what crop it is, and then will progress to the next stage if the day was reached
    public void GrowCrops()
    {
        //Checks to see if the plot is not not prepped state and that there is a crop on it
        if(plotHandler.plotStates != PlotStates.NotPrepped && currentCrop != Crops.None)
        {
            switch (currentCrop)
            {
                case Crops.carrot:
                    growthProgress = new int[3] {1, 2, 3};
                    break;
                case Crops.potato:
                    growthProgress = new int[3] { 3, 5, 7 };
                    break;
                case Crops.tomato:
                    growthProgress = new int[3] { 4, 7, 10 };
                    break;
                default:
                    growthProgress = new int[0];
                    break;
            }
            if (growthState == GrowthState.planted && timeManager.progressByDay == growthProgress[0])
            {
                growthState = GrowthState.baby;
            }
            else if (growthState == GrowthState.baby && timeManager.progressByDay == growthProgress[1])
            {
                growthState = GrowthState.teen;
            }
            else if(growthState == GrowthState.teen && timeManager.progressByDay == growthProgress[2])
            {
                growthState = GrowthState.adult;
            }
        }
        if(plotHandler.plotStates == PlotStates.NotPrepped)
        {
            growthState = GrowthState.planted;
            currentCrop = Crops.None;
            growthProgress = new int[0];
        }
    }
    //Function to make it so that whenever the time managers progress by day goes beyone 10, it will turn to 0, and constantly updates the local version to match the 
    public void CalculateDayProgress()
    {
        //Checks to see if the progress goes past day 10
        if (timeManager.progressByDay > 10)
        {
            //resets the day. It does this because the max growth progress is day 10, and will not be needed further on.
            timeManager.progressByDay = 0;
        }
        //Displays the time manager progress by day on a local variable for debugging purposes
        progressByDay = timeManager.progressByDay;
    }



    #endregion
    #region Harvesting Crops
    public void HarvestCrop()
    {
        if(growthState == GrowthState.adult)
        {
            growthState = GrowthState.planted;
            currentCrop = Crops.None;
            growthProgress = new int[0];
            

        }
    }
    #endregion
}
#region enums

//an enum to store the different states of the crop
public enum GrowthState
{
    planted,
    baby,
    teen,
    adult
}
//an enum to store the different crops
public enum Crops
{
    None,
    carrot,
    potato,
    tomato
}
#endregion
