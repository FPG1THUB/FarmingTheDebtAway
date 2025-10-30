using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour
{
    #region Variables
    [Header("Current Time Values")]
    public float timeElapsed; //float to measure time
    public int currentMinute;//int to measure what the current minute is
    public int currentHour;//int to measure what the current hour is
    public int currentDay;//int to measure what the current day is
    public int currentWeek;//int to measure what the current week is
    public int currentMonth;//int to measure what the current month is
    public int currentYear;//int to measure what the current year is

    [Header("Seasons")]
    public Season currentSeason;

    public enum Season//enum to store the different seasons in the year
    {
        Summer,
        Autumn,
        Winter,
        Spring
    }
;
    [Header("Time Durations")]
    public int minuteDuration = 1;//int to measure how long the minute will be
    public int hourDuration = 60;//int to measure how long many minutes will be in an hour
    public int dayDuration = 24;//int to measure how many hours will be in a day
    public int weekDuration = 7;//int to measure how many days will be in a week
    public int monthDuration = 4;//int to store how many days are in a month
    public int yearDuration = 12;//int to store how many months are in a year

    [Space(10), Header("Debugging")]
    public int timeStep = 1;
    #endregion
    #region Time Update Functions
    //Function to handle the updating time of minutes
    public void TimeUpdate()
    {
        //Makes it so that the timeElapsed increases per time instead of per frame once the function is called in Update
        timeElapsed += Time.deltaTime* timeStep;
        //Checks to see if the time elapsed has reached the minute duration, which is 1 by default so that it is faster
        while (timeElapsed >= minuteDuration)
        {
            //Resets the timeElapsed to 0
            timeElapsed = 0;
            //Activates the Add Minutes Function, which will add a minute to the currentMinute Function
            AddMinutes(1);
        }
    }
    //Function to add the minutes and hours
    public void AddMinutes(int minutesToAdd)
    {
        //Adds the minutes according to whatever number is in the function when it is called somewhere else
        currentMinute += minutesToAdd;
        //Checks to see if the minutes have reached how many minutes are in an hour, which is 60 by default
        while(currentMinute >= hourDuration)
        {
            //Subtracts the minutes from the hour duration
            currentMinute -= hourDuration;
            //Calls in the function to add hours, which is adding just 1 hour
            AddHours(1);
        }

    }
    //Function to add hours and days
    public void AddHours(int hoursToAdd)
    {
        //Adds the hours according to whatever number is in the function when it is called
        currentHour += hoursToAdd;
        //Checks to see if the hours have reached how many hours are in a day, which by default is 24
        while (currentHour >= dayDuration)
        {
            //subtracts the hours by the day duration
            currentHour -= dayDuration;
            //Calls in the function to add days
            AddDays(1);
        }
    }
    public void AddWeeks(int weeksToAdd)
    {
        currentWeek += weeksToAdd;
        while (currentWeek > monthDuration)
        {
            currentWeek -= monthDuration;
            AddMonths(1);
        }
    }
    public void AddDays(int daysToAdd)
    {
        currentDay += daysToAdd;
        while (currentDay >= weekDuration)
        {
            currentDay -= weekDuration;
            AddWeeks(1);
        }
    }
    public void AddMonths(int monthsToAdd)
    {
        currentMonth += monthsToAdd;
        while(currentMonth > yearDuration)
        {
            currentMonth -= yearDuration;
            AddYears(1);
        }
    }
    public void AddYears(int yearsToAdd)
    {
        currentYear += yearsToAdd;

    }
    #endregion

    #region Season Changing Functions
    //Function that handles the changing of seasons based on what month it is
    public void ChangeSeasons()
    {
        //Checks to see if the month is the first, otherwise known as January
        if(currentMonth == 1)
        {
            //Sets the season to Summer
            currentSeason = Season.Summer;
        }
        //If the month is not the first, check if the month is the 4th, which is otherwise known as April
        else if(currentMonth == 4)
        {
            //Sets the season to Autumn
            currentSeason = Season.Autumn;
        }
        //If the month is not the fourth, check if the month is the 7th, which is otherwise known as July
        else if(currentMonth == 7)
        {
            //Sets the season to Winter
            currentSeason = Season.Winter;
        }
        //If the month is not the 7th, check if the month is the 10t, which is otherwise known as October
        else if(currentMonth == 10)
        {
            //Sets the season to spring
            currentSeason = Season.Spring;
        }
    }
    #endregion
    // Update is called once per frame
    void Update()
    {
        TimeUpdate();
        ChangeSeasons();
    }
}
