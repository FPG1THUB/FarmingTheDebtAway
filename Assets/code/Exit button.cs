using UnityEngine;

public class Exitbutton : MonoBehaviour
{
    public void ExitButton()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
}
