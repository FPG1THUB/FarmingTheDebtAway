using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionManager : MonoBehaviour
{
    [SerializeField]
    private Dropdown _resolutionDropdown;

    private Resolution[] _availableResolutions;

    private int _currentResolutionIndex = -1;

    public int CurrentResolution
    {
        set { _currentResolutionIndex = value; }
        get { return _currentResolutionIndex; }
    }

    void Start()
    {
        _availableResolutions = Screen.resolutions;
        _resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < _availableResolutions.Length; i++)
        {
            string option = $"{_availableResolutions[i].width} x {_availableResolutions[i].height}";

            if (!options.Contains(option))
            {
                options.Add(option);
            }

            if (CurrentResolution == -1)
            {
                if (_availableResolutions[i].width == Screen.currentResolution.width &&
                    _availableResolutions[i].height == Screen.currentResolution.height)
                {
                    CurrentResolution = i;
                }
            }
        }

        _resolutionDropdown.AddOptions(options);

        _resolutionDropdown.value = CurrentResolution;

        _resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int selectedIndex)
    {
        CurrentResolution = selectedIndex;
        Resolution resolution = _availableResolutions[CurrentResolution];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
    }
}
