using System.IO;
using System.Xml.Linq;
using UnityEngine;

public class SaveAndLoadOptions : MonoBehaviour
{
    #region File Path and Data
    //file path JSON file path
    string _filepath = $"{Application.dataPath}/OptionsData.json";

    public OptionSaveData optionsData = new OptionSaveData();

    #endregion

    #region Refrence to additional scripts

    [Header("Options Scripts")]

    [SerializeField] AutioManager _autioManager;
    [SerializeField] FullscreenModeManager _fullscreenModeManager;
    [SerializeField] ResolutionManager _resolutionManager;
    [SerializeField] KeybindManager _keybindManager;


    #endregion

    #region Unity methods

    private void Awake()
    {
        if (File.Exists(_filepath))
        {
            LoadOptions();
        }
    }

    #endregion

    #region Save Options

    /// <summary>
    /// Collects the current settings from the various manager scripts
    /// and stores them into the optionsData object.
    /// </summary>
    void GetDataToSave()
    {




        // Save the names of actions for keybindings (e.g., "Forward", "Jumo").
        optionsData.keyNames = _keybindManager.SendKey();

        // Save the actual key values assigned to those actions (e.g., "W", "Space").
        optionsData.keyValues = _keybindManager.SendValue();

        // Save the current fullscreen mode (0 = Exclusive, 1 = Fullscreen Window, 2 = Windowed).
        optionsData.fullScreenMode = _fullscreenModeManager.CurrentFullscreenMode;

        // Save volume levels for different channels (e.g., Master, Music, SFX).
        optionsData.masterVolume = _autioManager._volume[0];
        optionsData.musicVolume = _autioManager._volume[1];
        optionsData.sfxVolume = _autioManager._volume[2];

        

    }
    /// <summary>
    /// Serializes the OptionSaveData object into a JSON string and saves it to a file.
    /// </summary>
    /// <param name="data">The data to save.</param>
    /// <param name="path">The file path where the data will be written.</param>
    void SaveJSON(OptionSaveData data, string path)
    {
        string lineToSave = JsonUtility.ToJson(data);       // Convert data to JSON string
        File.WriteAllText(path, lineToSave);                // Write string to file
    }

    /// <summary>
    /// Public method called to save all current settings to disk.
    /// Can be triggered by a UI button.
    /// </summary>
    public void SaveOptions()
    {
        GetDataToSave();                    // Gather current game settings
        SaveJSON(optionsData, _filepath);   // Save to file
    }

    #endregion

    #region Load Options

    /// <summary>
    /// Loads the JSON file from disk and deserializes it into an OptionSaveData object.
    /// </summary>
    /// <returns>The loaded OptionSaveData.</returns>
    OptionSaveData LoadData()
    {
        string loadedData = File.ReadAllText(_filepath);                     // Read JSON string
        return JsonUtility.FromJson<OptionSaveData>(loadedData);            // Convert JSON back to data object
    }

    /// <summary>
    /// Sends the loaded data back into the appropriate manager scripts.
    /// This restores the game settings visually and functionally.
    /// </summary>
    void SendDataFromLoad()
    {



        // Restore all keybinds using saved names and their corresponding keys (e.g., "Jump" = "Space").
        _keybindManager.SetUpLoadedKey(optionsData.keyNames, optionsData.keyValues);

        // Set the screen mode (e.g., Fullscreen, Windowed, Borderless) from saved data.
        _fullscreenModeManager.CurrentFullscreenMode = optionsData.fullScreenMode;

        // Restore the volume levels (Master) from saved float array.
        _autioManager._volume[0] = optionsData.masterVolume;
        _autioManager._volume[1] = optionsData.musicVolume;
        _autioManager._volume[2] = optionsData.sfxVolume;

        
    }

    /// <summary>
    /// Public method to load and apply saved options from disk.
    /// Can be triggered on game start or from an options menu.
    /// </summary>
    public void LoadOptions()
    {
        optionsData = LoadData();      // Load from disk
        SendDataFromLoad();            // Apply to game
    }

    #endregion   
}



