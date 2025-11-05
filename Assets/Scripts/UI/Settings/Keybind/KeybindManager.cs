using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class KeybindManager : MonoBehaviour
{
    [Serializable]
    public struct ActionMapData
    {
        public string actionName;
        public Text keycodeDisplay;
        public string defaultkey;
    }

    [Header("Action Mapping")]
    [SerializeField] ActionMapData[] _actionMapData;
    [Header("UI Feedback")]
    [SerializeField] GameObject _currentSelectedKey;
    public static Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    [SerializeField] private Color32 _selectedKey = new Color32(239, 116, 36, 225);
    [SerializeField] private Color32 _changedKey = new Color32(39, 171, 249, 225);

    public void SetUpLoadedKey(string[] key, string[] value)
    {
        keys.Clear();
        for (int i = 0; i < key.Length; i++)
        {
            keys.Add(key[i], (KeyCode)Enum.Parse(typeof(KeyCode), value[i]));
        }
    }

    public string[] SendKey()
    {
        string[] tempkey = new string[keys.Count];
        int i = 0;
        foreach (KeyValuePair<string, KeyCode> key in keys)
        {
            tempkey[i] = key.Key;
            i++;
        }
        return tempkey;
    }
    public string[] SendValue()
    {
        // Create an array to hold the key values.
        string[] tempValue = new string[keys.Count];
        // Create an int to hold the current array element.
        int i = 0;
        // Loop through the dictionary to extract key values.
        foreach (KeyValuePair<string, KeyCode> key in keys)
        {
            // Store the key value (KeyCode) as a string in the array.
            tempValue[i] = key.Value.ToString();
            // Move to the next index.
            i++;
        }
        // Return the key values.
        return tempValue;
    }

    private void Start()
    {
        for (int i = 0; i < _actionMapData.Length; i++)
        {
            if (!keys.ContainsKey(_actionMapData[i].actionName))
            {
                keys.Add(_actionMapData[i].actionName, (KeyCode)Enum.Parse(typeof(KeyCode), _actionMapData[i].defaultkey));
            }
            _actionMapData[i].keycodeDisplay.text = keys[_actionMapData[i].actionName].ToString();
        }
    }

    public void changeKey(GameObject clickedKey)
    {
        _currentSelectedKey = clickedKey;
        if (_currentSelectedKey != null)
        {
            _currentSelectedKey.GetComponent<Image>().color = _selectedKey;
        }
    }

    private void OnGUI()
    {
        Event changeKeyEvent = Event.current;
        if (_currentSelectedKey != null && changeKeyEvent.isKey)
        {
            if (!keys.ContainsValue(changeKeyEvent.keyCode))
            {
                keys[_currentSelectedKey.name] = changeKeyEvent.keyCode;
                _currentSelectedKey.GetComponentInChildren<Text>().text = changeKeyEvent.keyCode.ToString();
                _currentSelectedKey.GetComponent<Image>().color = _changedKey;
                _currentSelectedKey = null;
            }
        }
    }
}
