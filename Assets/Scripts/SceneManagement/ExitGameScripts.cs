using UnityEngine;

public class ExitGameScripts : MonoBehaviour
{
    // Only assessed on the actual game, so it's ok to copy this section.
public void ExitToDesktop()
    {
#if UNITY_EDITOR // only happens in the editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
}
