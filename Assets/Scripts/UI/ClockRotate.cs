using UnityEngine;
using UnityEngine.UI;
public class ClockRotate : MonoBehaviour
{
    public TimeManager time;
    public GameObject clockHandle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = GameObject.FindGameObjectWithTag("Time Manager").GetComponent<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
       RotateClock(); 
    }
    /// <summary>
    /// This function will rotate the handle of the clock according to the hour in game
    /// </summary>
    public void RotateClock()
    {
        float totalMinute = time.currentHour * time.hourDuration + time.currentMinute + time.timeElapsed / time.minuteDuration;
        float rotationAngle =   -((totalMinute / (time.hourDuration * time.dayDuration)) * 360);
        clockHandle.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
    }
}
