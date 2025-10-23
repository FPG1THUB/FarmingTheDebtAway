using UnityEngine;
using System; 

public class DefaultIfKeybindsAreMissing : MonoBehaviour
{
    // Calls this when the script is being loaded immediately.
    private void Awake()
    {
        
        if (KeybindManager.keys.Count <= 0) // Checks if there are no bound keys in the KeyBinds Script
        {
            for (int i = 0; i < DefaultControls.keyNames.Length; i++) // Goes through each of the default control names
            {
                // this adds the default keybinding's action name and string to the keybinding dictionary
                KeybindManager.keys.Add(DefaultControls.keyNames[i], (KeyCode)Enum.Parse(typeof(KeyCode), DefaultControls.keyValues[i]));
            }
        }
    }
}
