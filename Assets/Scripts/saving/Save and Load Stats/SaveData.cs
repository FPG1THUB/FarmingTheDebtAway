using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Class to store the stats of the game. Will be used to save and load the data between the Save
/// and load stats script and the JSON file
/// </summary>
public class SaveData
{
    #region Watering
    //Float to store the current max water and water speed, which can be changed through upgrading the water bucket
    public float maxWater;
    public int waterSpeed;
    #endregion
    #region Plot and Crops
    //list of the plot states for all of the plots
    public PlotStates[] plotState = new PlotStates[48];
    //list of all the crop states for all of the crops
    public GrowthState[] growthStates = new GrowthState[48];
    //list of all the current crops one each plot
    public Crops[] currentCrop = new Crops[48];
    #endregion
    #region Currency and Shop
    //int to store the amount of money the player has
    public int money;
    //int to store the amount it costs to upgrade the water bucket
    public int waterUpgradeAmount;
    #endregion
    #region Inventory
    //list to store all of the items in the players inventory
    public int[] itemIDs = new int[8];
    public int[] itemAmounts = new int[8];
    #endregion
    #region Time
    //ints to store the in game time
    public int minute;
    public int hour;
    public int day;
    public int week;
    public int month;
    public int year;
    #endregion

}
