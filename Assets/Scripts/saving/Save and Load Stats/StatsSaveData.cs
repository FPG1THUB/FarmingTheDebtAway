using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class to store the stats of the game. Will be used to save and load the data between the Save
/// and load stats script and the JSON file
/// </summary>
public class StatsSaveData
{
    #region Watering
    public float maxWater;
    public int waterSpeed;
    #endregion
    #region Plot and Crops
    public PlotHandler[] plotState = new PlotHandler[48];
    public CropHandler[] cropState = new CropHandler[48];
    #endregion
    #region Currency and Shop
    public int money;
    public int waterUpgradeAmount;
    #endregion
    #region Inventory
    public List<Item> inventory;
    #endregion
    #region Time
    public int minute;
    public int hour;
    public int day;
    public int week;
    public int month;
    public int year;
    #endregion

}
