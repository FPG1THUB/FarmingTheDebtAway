using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.GlobalIllumination;

public class TimeManager : MonoBehaviour
{
    #region Variables
    [Space(10), Header("Current Time Values")]
    public float timeElapsed; //float to measure time
    public int currentMinute;//int to measure what the current minute is
    public int currentHour;//int to measure what the current hour is
    public int currentDay;//int to measure what the current day is
    public int currentWeek;//int to measure what the current week is
    public int currentMonth;//int to measure what the current month is
    public int currentYear;//int to measure what the current year is
    [Space(10), Header("Seasons")]
    public Season currentSeason;

    public enum Season//enum to store the different seasons in the year
    {
        Summer,
        Autumn,
        Winter,
        Spring
    }
;
    [Space(10), Header("Time Durations")]
    public int minuteDuration = 1;//int to measure how long the minute will be
    public int hourDuration = 60;//int to measure how long many minutes will be in an hour
    public int dayDuration = 24;//int to measure how many hours will be in a day
    public int weekDuration = 7;//int to measure how many days will be in a week
    public int monthDuration = 4;//int to store how many days are in a month
    public int yearDuration = 12;//int to store how many months are in a year

    [Space(10), Header("Debugging")]
    public int timeStep = 1;

    [Space(10), Header("Light Manipulation")]
    public Light light;
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
            //Calls in the function to add day by 1
            AddDays(1);
        }
    }
    //Function to add the weeks and months
    public void AddWeeks(int weeksToAdd)
    {
        //adds the weeks according to how many weeks it wants to add when it gets called 
        currentWeek += weeksToAdd;
        //Checks to see if the weeks have reached how many weeks are in a month, which by default is 4
        while (currentWeek > monthDuration)
        {
            //subtracts the current amount of weeks from the month duration, which is 4
            currentWeek -= monthDuration;
            //adds 1 month 
            AddMonths(1);
        }
    }
    //function to add the days and weeks
    public void AddDays(int daysToAdd)
    {
        //adds the days according to how many days it wants to add when it gets called
        currentDay += daysToAdd;
        //Checks to see if the days have reached how many days are in a week, which by default is 7
        while (currentDay >= weekDuration)
        {
            //subtracts the current amount of days from the week duration, which is 7
            currentDay -= weekDuration;
            //adds 1 week
            AddWeeks(1);
        }
    }
    //function to add the months and years
    public void AddMonths(int monthsToAdd)
    {
        //adds the months according to how many months it wants to add when it gets called
        currentMonth += monthsToAdd;
        //Checks to see if the days have reached how many months are in a year, which by default is 12
        while (currentMonth > yearDuration)
        {
            //subtracts the current amount of months from the year duration, which is 12
            currentMonth -= yearDuration;
            //adds 1 year
            AddYears(1);
        }
    }
    //function to add the years
    public void AddYears(int yearsToAdd)
    {
        //adds the years according to how many years it wants to add when it gets called
        currentYear += yearsToAdd;

    }
    #endregion
    #region RotateLight
    public void RotateLight()
    {
        //Checks to see if the light is attached to anything
        if(light != null)
        {
            //Calculates the total minutes that passed in the current day by turning the amount of hours passed into minutes, then adding the current minutes, plus the minutes that are currently ongoing
            float totalMinutes = currentHour * hourDuration + currentMinute + timeElapsed / minuteDuration;
            //Calculates the rotation by dividing the total minutes passed by the results of day duration(24) * hourDuration(60), then times that result by 360
            float rotationAngle = (totalMinutes / (dayDuration * hourDuration) * 360f);
            //Sets the directional lights rotation by taking the results of the rotation angle and subtracting it by 90 to set the z Axis, then setting the y axis as 1170 and the x axis as 0
            light.transform.rotation = Quaternion.Euler(rotationAngle - 90f, 170f, 0f);
        }
        else
        {
            Debug.LogError("There is not light attached to the time script!");
        }
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
    #region Unity Callbacks
    // Update is called once per frame
    void Update()
    {
        TimeUpdate();
        ChangeSeasons();
    }
    private void LateUpdate()
    {
        RotateLight();
    }
    #endregion
}
