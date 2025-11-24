using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class PlotHandler : MonoBehaviour, Interactable
{
    #region Variables
    //Handles the inventory manager to fetch the tools and seeds
    public Inventory inventoryManager;
    //Handles the watering manager to fetch the amount of water
    public Watering wateringManager;
    //Handles the time manager to fetch the current time
    public TimeManager timeManager;
    //References the 3 plot states as prefabs
    public GameObject[] plotPrefabs = new GameObject[3];
    //References the current state of the plot
    public PlotStates plotStates = PlotStates.NotPrepped;
    //References the progress of the watering plot in the form of a float so that Time.deltaTime can be used on it
    public float floatWaterProgress;
    //Stores the water progress in an int form
    public int waterProgress;
    //Sets the time it should take to water
    public int timeTakesToWaterPlot = 3;
    #endregion
    #region Unity Callbacks
    //Called on the first frame
    private void Start()
    {
        //Loops through each plot prefab that exists
        for (int i = 0; i < plotPrefabs.Length; i++)
        {
            //retrieves the plot prefabs
            plotPrefabs[i] = this.transform.GetChild(i).gameObject;
        }
        //Switches the state of the plot
        SwitchStates();
        //Retrieves the watering manager from the manager object
        wateringManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Watering>();
        //Retrieves the inventory manager from the manager object
        inventoryManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Inventory>();
        //retrieves the time manager from the manager object
        timeManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<TimeManager>();
    }
    //called on every frame
    public void Update()
    {
        //Switches states if there has been change
        SwitchStates();
    }
    #endregion
    #region Plot States Functions
    //Function to switch the state if the enum is set to a specific state
    public void SwitchStates()
    {
        //stores the current plot state
        int currentState = 0;
        //passes the plot states through a switch statement
        switch (plotStates)
        {
            //It first checks to see if the plot state is set to not prepped
            case PlotStates.NotPrepped:
                //if so, the current state will be set to 1
                currentState = 1;
                break;
                //checks to see if the plot state is set to dry
            case PlotStates.Dry:
                //if so, the current state will be set to 0
                currentState = 0;
                break;
                //checks to see if the plot state is set to wet
            case PlotStates.Wet:
                //if so, set current state to 2
                currentState = 2;
                break;

            default:
                //by default, the current state will be dry, aka 0
                currentState = 0;
                break;
        }
        //Goes through each plot state object
        for (int i = 0; i < plotPrefabs.Length; i++)
        {
            //Sets them all inactive
            plotPrefabs[i].SetActive(false);
        }
        //sets the current state of the plot active
        plotPrefabs[currentState].SetActive(true);

    }
    //Function to perform actions based on the tool equipped
    public void UseItem()
    {
        //Checks to see if the currently equipped item is the hoe
        if (inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemName == "Hoe")
        {
            //checks to see if the state is set to not prepped
            if (plotStates == PlotStates.NotPrepped)
            {
                //if so, sets the plot state to dry
                plotStates = PlotStates.Dry;
                //Changes the state of the plot to dry in the scene
                SwitchStates();
            }
        }
        //Checks to see if the currently equipped item is the watering can
        if (inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemName == "Watering Can")
        {
            //Checks to see if the current plot state is dry
            if (plotStates == PlotStates.Dry)
            {
                //checks to see if there is any water in the watering can
                if (wateringManager.currentWaterAmount >= 0)
                {
                    //if so, it will start emptying the can based on time and water speed
                    wateringManager.currentWaterAmount -= Time.deltaTime * wateringManager.waterSpeed;
                    //while emptying the water, it will also progress watering the plot in float format so that it can be calculated with time.deltatime
                    floatWaterProgress += Time.deltaTime *wateringManager.waterSpeed;
                    //checks to see if the float water progress has reached 1
                    if (floatWaterProgress >= 1)
                    {
                        //if so, add water progress by 1 and remove float water progress by 1
                        waterProgress++;
                        floatWaterProgress--;
                    }
                    //Checks to see if the water progress has reached the time it takes to water the plot
                    if (waterProgress == timeTakesToWaterPlot)
                    {
                        //Sets the plot state to wet
                        plotStates = PlotStates.Wet;
                        //Resets the float water progress and water progress
                        floatWaterProgress = 0;
                        waterProgress = 0;
                    } 
                }
            }
        }
    }
    //Function to change the plot state based on if the time has passed(calculated in the timeskip script), and the current state 
    public void SwitchPlotStateBasedByTime()
    {
        //Checks to see if the plot is wet
        if (plotStates == PlotStates.Wet)
        {
            //if so, set it to dry
            plotStates = PlotStates.Dry;
        }
        //otherwise, checks tos ee if the plot state is dry
        else if (plotStates == PlotStates.Dry)
        {
            //if so, set it to not prepped
            plotStates = PlotStates.NotPrepped;
        }

    }
    #endregion
    #region Interactable functions
    //occurs when trying to interact with something that has this script attached to it
    public void OnInteraction()
    {
        //calls the use item function
        UseItem();
    }
    //Occurs when trying to interact with something, and wants to display something
    public string ToolTip()
    {
        //checks to see if the plot is unprepped
        if (plotStates == PlotStates.NotPrepped)
        {
            //checks to see if the currently equipped item is the hoe
            if(inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemName == "Hoe")
            {
                //if so, it will display "Press E to till"
                return "Press E to till";
            }
        }
        //if not, then checks to see if the current plot state is dry
        else if(plotStates == PlotStates.Dry)
        {
            //checks to see if the currently equipped item is the watering can
            if(inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemName == "Watering Can")
            {
                //displays "Press E to water"
                return "Hold E to water";
            }
        }
        //else display nothing
        return null;
    }
    #endregion

}
#region Enums
//Enum to store what the current state of the plot is
public enum PlotStates
{
    NotPrepped,
    Dry,
    Wet
}
#endregion