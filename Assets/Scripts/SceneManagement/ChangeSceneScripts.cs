using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneScripts : MonoBehaviour
{
public void ChangeSceneByName(string sceneName) //Option 1
    {
        SceneManager.LoadScene(sceneName); // Changes to different scene
    }
    public void ChangeSceneByIndex(int sceneIndex) // Option 2, both work
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void ReloadCurrentScene() // Reloads the current Scene
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadNextScene() // Loads the next scene in the list
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);

    }
}
