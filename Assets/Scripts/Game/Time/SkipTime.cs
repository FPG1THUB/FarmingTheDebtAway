using UnityEngine;

public class SkipTime : MonoBehaviour, Interactable
{
    //Reference to the time manager script
    public TimeManager timeManager;
    [SerializeField]PlotHandler[] plotHandlers;
    //is called on once the player has come into contact and attempted to interact with an object that is interactable
    public void OnInteraction()
    {
        //Makes it so that the player skips the
        SkiptheTime();
        //displays in the debug log that time has skipped
        Debug.Log("Time skipped");
    }
    //displays what to do once the player has come into contact with the interactable object
    public string ToolTip()
    {
        //displays "Press E to skip time"
        return "Press E to skip time";
    }
    //Called on the first frame of the game
    private void Start()
    {
        //Finds and attaches the time manager script onto the time manager variable
        timeManager = GameObject.FindGameObjectWithTag("Time Manager").GetComponent<TimeManager>();
         plotHandlers = FindObjectsByType<PlotHandler>(FindObjectsInactive.Exclude,FindObjectsSortMode.None);
        SkiptheTime();
    }
    //function to skip the time to 1 day ahead at 6am in the morning
    public void SkiptheTime()
    {
        //adds 1 day to the current time, and if it ends up being 8 days or more, then it will add to a week
        timeManager.AddDays(1);
        //Sets the current hour to 6
        timeManager.currentHour = 6;
        //Sets the current minute to 0
        timeManager.currentMinute = 0;
        //Trigger plot code here...Figure that out later...
        /*
            issue 
            need to shout at all plots in scene to run code...
            actually not issue think i have an idea...remind me of idea later
         
         */
        foreach (PlotHandler plot in plotHandlers)
        {
            plot.SwitchPlotStateBasedByTime();
        }
    }
    void Update()
    {
        if(timeManager.currentHour == 2 && timeManager.currentMinute >= 30)
        {
            //probs look into using a coroutine ... research that shit yea
            //Pause time
            //fade to black
            //make a comment about being tired
            SkiptheTime();
            //un pause time
            //fade to transparent ...back to game
        }
    }
}
