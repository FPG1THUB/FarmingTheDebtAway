using UnityEngine;

public class TimePause : MonoBehaviour
{
    public void PauseTime()
    {
        Time.timeScale = 0;
    }
    public void UnpauseTime()
    {
        Time.timeScale = 1f;
    }
}
