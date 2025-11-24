using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class PlotHandler : MonoBehaviour, Interactable
{
    //Handles the inventory manager to fetch the tools and seeds
    public Inventory inventoryManager;
    //Handles the watering manager to fetch the amount of water
    public Watering wateringManager;
    //Handles the time manager to fetch the current time
    public TimeManager timeManager;
    public GameObject[] plotPrefabs = new GameObject[3];
    public PlotStates plotStates = PlotStates.NotPrepped;
    public float floatWaterProgress;
    public int waterProgress;
    public int timeTakesToWaterPlot = 3;

    private void Start()
    {
        for (int i = 0; i < plotPrefabs.Length; i++)
        {
            plotPrefabs[i] = this.transform.GetChild(i).gameObject;
        }
        SwitchStates();
        wateringManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Watering>();
        inventoryManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Inventory>();
        timeManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<TimeManager>();
    }
    public void SwitchStates()
    {
        int currentState = 0;
        switch (plotStates)
        {
            case PlotStates.NotPrepped:
                currentState = 1;
                break;
            case PlotStates.Dry:
                currentState = 0;
                break;
            case PlotStates.Wet:
                currentState = 2;
                break;

            default:
                currentState = 0;
                break;
        }

        for (int i = 0; i < plotPrefabs.Length; i++)
        {
            plotPrefabs[i].SetActive(false);
        }
        plotPrefabs[currentState].SetActive(true);

    }
    public void Update()
    {
        SwitchStates();
    }
    public void UseItem()
    {
        if (inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemName == "Hoe")
        {
            if (plotStates == PlotStates.NotPrepped)
            {
                plotStates = PlotStates.Dry;
                SwitchStates();
            }
        }
        if (inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemName == "Watering Can")
        {
            if (plotStates == PlotStates.Dry)
            {
                if (wateringManager.currentWaterAmount >= 0)
                {
                    //Come back to this, it works but it waters the plot too fast
                    wateringManager.currentWaterAmount -= Time.deltaTime * wateringManager.waterSpeed;
                    floatWaterProgress += Time.deltaTime * wateringManager.waterSpeed;
                    if (floatWaterProgress >= 1)
                    {
                        waterProgress++;
                    }
                    if (waterProgress == timeTakesToWaterPlot)
                    {
                        plotStates = PlotStates.Wet;
                        floatWaterProgress = 0;
                        waterProgress = 0;
                    }
                    
                }

                Debug.Log("Water the Plot Slot");
            }
        }
    }

    public void OnInteraction()
    {
        UseItem();
    }

    public string ToolTip()
    {
        if (plotStates == PlotStates.NotPrepped)
        {
            if(inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemName == "Hoe")
            {
                return "Press E to till";
            }
        }
        else if(plotStates == PlotStates.Dry)
        {
            if(inventoryManager.inventory[inventoryManager._selectedHotbarIndex].ItemName == "Watering Can")
            {
                return "Hold E to water";
            }
        }
        return null;
    }
    public void SwitchPlotStateBasedByTime()
    {
        if(timeManager.currentHour == 0 || timeManager.currentHour == 6)
        {
            Debug.Log("Next Day");
        }
    }
}

public enum PlotStates
{
    NotPrepped,
    Dry,
    Wet
}
