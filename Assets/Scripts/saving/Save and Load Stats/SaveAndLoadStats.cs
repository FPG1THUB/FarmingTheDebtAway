using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
public class SaveAndLoadStats : MonoBehaviour
{
    #region Variables
    StatsSaveData saveData = new StatsSaveData();
    private string _filePath;
    public Watering waterScript;
    public Inventory inventoryScript;
    public Transaction transactionScript;
    public TimeManager timeScript;
    public List<PlotHandler> plotScripts = new List<PlotHandler>();
    public List<CropHandler> cropScripts = new List<CropHandler>();
    #endregion
    #region Unity Callbacks
    private void Awake()
    {
        waterScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<Watering>();
        inventoryScript = GameObject.FindGameObjectWithTag("Inventory Manager").GetComponent<Inventory>();
        transactionScript = GameObject.FindGameObjectWithTag("Water Can Upgrade").GetComponent<Transaction>();
        timeScript = GameObject.FindGameObjectWithTag("Time Manager").GetComponent<TimeManager>();
        plotScripts.AddRange(FindObjectsByType<PlotHandler>(FindObjectsSortMode.None));
        cropScripts.AddRange(FindObjectsByType<CropHandler>(FindObjectsSortMode.None));
    }
    private void Start()
    {
        _filePath = $"{Application.dataPath}/SaveSlot1.json";
    }
    #endregion
    #region Send and Get Data
    // <summary>
    /// Sends the data from all of the scripts into the StatsData script
    /// </summary>
    public void GetData()
    {
        plotScripts.CopyTo(saveData.plotState);
        Debug.Log(saveData.plotState);
    }
    /// <summary>
    /// Retrieves the data from the StatsData script and puts them into the other scripts
    /// </summary>
    public void SendData()
    {

    }
    public void SetFilePath(string filePath)
    {
        _filePath = $"{Application.dataPath}/{filePath}.json";
    }
    #endregion
    #region Read and Save Data
    /// <summary>
    /// will put all of the stats into the stats data script, then put it into a JSON
    /// </summary>
    public void SaveData()
    {
        GetData();
        string dataToSave = JsonUtility.ToJson(saveData);
        File.WriteAllText(_filePath, dataToSave);
        Debug.Log("Data saved!");
        Debug.Log($"location of save file:{_filePath}");
        
    }
    /// <summary>
    /// Will take out all of the information from an existing JSON file and put it into
    /// The stats data script, then will put it into all of the other scripts
    /// </summary>
    public void LoadData(string filePath)
    {
        
    }
    #endregion
}