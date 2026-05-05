using UnityEngine;
using System.Collections.Generic;
using System.IO;
/// <summary>
/// In charge of handling the saving and loading of the stats, including:
/// Time,
/// Money,
/// Watering can upgrades,
/// plot states,
/// crop states,
/// </summary>
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
    public Item itemClass = new Item();
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
        //Important thing to note, the Find objects sort mode has to be instance ID as to make sure
        //the scripts are consistently stored in the same order so that they are saved and loaded
        //in the correct order
        plotScripts.AddRange(FindObjectsByType<PlotHandler>(FindObjectsSortMode.InstanceID));
        //finds all of the crop handler scripts that would be attached to all the plots, and stores them in the crop scripts list
        cropScripts.AddRange(FindObjectsByType<CropHandler>(FindObjectsSortMode.InstanceID));

    }

    private void Start()
    {
        //by default, the file path will be save slot 1
        Debug.Log(_filePath);
        //Checks to make sure there isnt an existing save file
        if (File.Exists(_filePath))
        {
            //if so, load it
            LoadStats();
        }
    }
    #endregion
    #region Set File Path Function
    /// <summary>
    /// Sets the file path so that the script can identify which save file to save and load on
    /// </summary>
    public void SetFilePath(string filePath)
    {
        //Sets the file path to the allocated save slot which will be stated in the inspector
        _filePath = $"{Application.dataPath}/{filePath}.json";
        //Little Debug.Log to state what the file path is to make sure it is fetching the right save file
        Debug.Log(_filePath);
    }
    #endregion
    #region Save Data
    /// <summary>
    /// will put all of the stats into the stats data script, then put it into a JSON format
    /// </summary>
    private void ConvertData(SaveData saveData, string filePath)
    {
        //creates a temp variable that turns all of the data in the savedata class into Json format
        string dataToSave = JsonUtility.ToJson(saveData);
        //writes all of the data into a Json file and puts it onto the specified file path
        File.WriteAllText(_filePath, dataToSave);
        //little debug log to shout the saved stats to make life a little easier so devs can see whether its saving correct stuff
        Debug.Log(dataToSave);

    }
    /// <summary>
    /// Takes all the information in the save data class and puts it into scripts to effectively load the data
    /// </summary>
    private void GetData()
    {
        //Stores all of the current data into the save data script, including time, money, water upgrade, and plot and crop states
        saveData.minute = timeScript.currentMinute;
        saveData.hour = timeScript.currentHour;
        saveData.day = timeScript.currentDay;
        saveData.month = timeScript.currentMonth;
        saveData.year = timeScript.currentYear;
        saveData.money = inventoryScript.money;
        saveData.waterUpgradeAmount = transactionScript.moneyValue;
        saveData.waterSpeed = waterScript.waterSpeed;
        saveData.maxWater = waterScript.maxWaterAmount;
        for (int i = 0; i < plotScripts.Count; i++)
        {
            PlotStates plotState = plotScripts[i].plotStates;
            saveData.plotState[i] = plotState;
        }
        for (int i = 0; i < cropScripts.Count; i++)
        {
            Crops currentCrop = cropScripts[i].currentCrop;
            GrowthState growthState = cropScripts[i].growthState;
            saveData.currentCrop[i] = currentCrop;
            saveData.growthStates[i] = growthState;
        }
        for(int i = 0; i < inventoryScript.inventory.Count; i++)
        {
            saveData.itemIDs[i] = inventoryScript.inventory[i].ItemId;
            saveData.itemAmounts[i] = inventoryScript.inventory[i].ItemQuantity;
        }


    }
    /// <summary>
    /// Public function to save the current stats
    /// </summary>
    public void SaveStats()
    {
        GetData();
        ConvertData(saveData, _filePath);
        Debug.Log(saveData);
    }
    #endregion
    #region LoadData
    /// <summary>
    /// Takes the information from the save file and puts it into the save data class
    /// </summary>
    private void LoadJsonFile()
    {
        //Creates a temporary string to read all of the save file data and turn it into a string
        string dataToLoad = File.ReadAllText(_filePath);
        //Puts the temporary string into the saveData class
        saveData = JsonUtility.FromJson<SaveData>(dataToLoad);
        //Debug to make sure it is working on paper
        Debug.Log(saveData);
    }
    /// <summary>
    /// Retrieves the data from the StatsData script and puts them into the other scripts
    /// </summary>
    private void LoadSaveData()
    {
        //sets the time, money, water upgrade, and plot and crop states to what the save data class states
        timeScript.currentMinute = saveData.minute;
        timeScript.currentHour = saveData.hour;
        timeScript.currentDay = saveData.day;
        timeScript.currentWeek = saveData.week;
        timeScript.currentMonth = saveData.month;
        timeScript.currentYear = saveData.year;
        inventoryScript.money = saveData.money;
        transactionScript.moneyValue = saveData.waterUpgradeAmount;
        waterScript.waterSpeed = saveData.waterSpeed;
        waterScript.maxWaterAmount = saveData.maxWater;
        for (int i = 0; i < plotScripts.Count; i++)
        {
            plotScripts[i].plotStates = saveData.plotState[i];
        }
        for (int i = 0; i < cropScripts.Count; i++)
        {
            cropScripts[i].currentCrop = saveData.currentCrop[i];
            cropScripts[i].growthState = saveData.growthStates[i];
        }
        for(int i = 0; i < inventoryScript.inventory.Count; i++)
        {
            inventoryScript.inventory[i].ItemId = saveData.itemIDs[i];
            inventoryScript.inventory[i].ItemQuantity = saveData.itemAmounts[i];
        }
        inventoryScript.UpdateHotBarDisplay();
        inventoryScript.UpdateCurrency(0);
    }
    /// <summary>
    /// public function to load the json file of the current save then load it into the game
    /// </summary>
    public void LoadStats()
    {
        LoadJsonFile();
        LoadSaveData();
        Debug.Log("Data Loaded!!!");
    }
    #endregion
}