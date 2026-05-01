using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
public class SaveAndLoadStats : MonoBehaviour
{
    #region Variables
    //Creates a variable to access the save data script
    SaveData saveData = new SaveData(); 
    //string used to find the file path of the current save file
    private static string _filePath;
    //reference to the water script to retrieve and load water capacity and speed
    public Watering waterScript;
    //Inventory script references to retrieve and laod teh current items in hotbar and current amount of moeny
    public Inventory inventoryScript;
    //Transaction script to retrieve and laod the current cost of water bucket upgrade
    public Transaction transactionScript;
    //Time script to retrieve and load the time
    public TimeManager timeScript;
    //plot scripts list to store the states of all the plot
    public List<PlotHandler> plotScripts = new List<PlotHandler>();
    //crop scripts list to store the states of all the currently equipped crops
    public List<CropHandler> cropScripts = new List<CropHandler>();
    #endregion
    #region Unity Callbacks
    private void Awake()
    {
        //attaches the water script into this script
        waterScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<Watering>();
        //attaches the inventory script into this script
        inventoryScript = GameObject.FindGameObjectWithTag("Inventory Manager").GetComponent<Inventory>();
        //attaches the transaction script into this script
        transactionScript = GameObject.FindGameObjectWithTag("Water Can Upgrade").GetComponent<Transaction>();
        //attaches the time script into this scripts
        timeScript = GameObject.FindGameObjectWithTag("Time Manager").GetComponent<TimeManager>();
        //finds all of the plot handler scripts that would be attached to all the plots, and stores them ion the plot scripts list
        plotScripts.AddRange(FindObjectsByType<PlotHandler>(FindObjectsSortMode.None));

        //finds all of the crop handler scripts that would be attached to all the plots, and stores them in the crop scripts list
        cropScripts.AddRange(FindObjectsByType<CropHandler>(FindObjectsSortMode.None));
    }

    private void Start()
    {
        //by default, the file path will be save slot 1
        Debug.Log(_filePath);
    }
    #endregion
    #region Send and Get Data
    /// <summary>
    /// Sends the data from all of the scripts into the StatsData script
    /// </summary>
    public void GetData()
    {
        saveData.minute = timeScript.currentMinute;
        saveData.hour = timeScript.currentHour;
        saveData.day = timeScript.currentDay;
        saveData.month = timeScript.currentMonth;
        saveData.year = timeScript.currentYear;
        saveData.money = inventoryScript.money;
        saveData.waterUpgradeAmount = transactionScript.moneyValue;
        saveData.waterSpeed = waterScript.waterSpeed;
        saveData.maxWater = waterScript.maxWaterAmount;
        for(int i = 0; i < plotScripts.Count; i++)
        {
            PlotStates plotState = plotScripts[i].plotStates;
            saveData.plotState[i] = plotState;
        }
        for(int i = 0; i < cropScripts.Count; i++)
        {
            Crops currentCrop = cropScripts[i].currentCrop;
            GrowthState growthState = cropScripts[i].growthState;
            saveData.currentCrop[i] = currentCrop;
            saveData.growthStates[i] = growthState;
        }
        

    }
    /// <summary>
    /// Retrieves the data from the StatsData script and puts them into the other scripts
    /// </summary>
    //Havent begun, still working on getting the data and saving it
    public void LoadData()
    {

    }
    /// <summary>
    /// void to set the file path according to what the save slot is(e.g. if its save slot 2 we will be using teh save slot 2 file)
    /// </summary>
    /// <param name="filePath"></param>
    // the main issue is that in having the function here, it would need to exist in the main menu scene, but then it would also need to exist
    //in the game scene too, while retaining data
    public void SetFilePath(string filePath)
    {
        //Sets the file path to the allocated save slot which will be stated int he inspector
        _filePath = $"{Application.dataPath}/{filePath}.json";
        Debug.Log(_filePath);
    }
    #endregion
    #region Read and Save Data
    /// <summary>
    /// will put all of the stats into the stats data script, then put it into a JSON
    /// </summary>
    public void ConvertData(SaveData saveData, string filePath)
    {
        //creates a temp variable that turns all of the data in the savedata class into Json format
        string dataToSave = JsonUtility.ToJson(saveData);
        //writes all of the data into a Json file and puts it onto the specified file path
        File.WriteAllText(_filePath, dataToSave);
        //little debug log to state that this function and all its actions have been run
        Debug.Log("Data saved!");
        Debug.Log(dataToSave);

    }
    public void SaveStats()
    {
        GetData();
        ConvertData(saveData, _filePath);
    }
    #endregion
}