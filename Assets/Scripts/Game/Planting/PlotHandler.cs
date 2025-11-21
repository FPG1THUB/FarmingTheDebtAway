using UnityEngine;
using System.Collections.Generic;

public class PlotHandler : MonoBehaviour
{
    public Inventory inventoryManager;
    public Watering wateringManager;
    public GameObject[] plotPrefabs = new GameObject[3];
    public PlotStates plotStates = PlotStates.NotPrepped;

    private void Start()
    {
        for (int i = 0; i < plotPrefabs.Length; i++)
        {
            plotPrefabs[i] = this.transform.GetChild(i).gameObject;
        }
        SwitchStates();
        wateringManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Watering>();
        inventoryManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Inventory>();
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
    public void UseItems()
    {
        if (inventoryManager._selectedHotbarIndex == 0)
        {

        }
    }
    public void Update()
    {
        SwitchStates();
    }
}

public enum PlotStates
{
    NotPrepped,
    Dry,
    Wet
}
