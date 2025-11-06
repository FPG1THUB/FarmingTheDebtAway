using UnityEngine;// Required for using Unity-specific types and functions.
using System;// Required to make custom data types savable and visible in the Inspector with [Serializable].


/// <summary>
/// Serializable data container for saving player options such as
/// resolution, audio, quality, and control settings.
/// </summary>
[Serializable]
public class OptionSaveData
{
    #region Resolution and Fullscreen

    [Header("Resolution Fullscreen")]

    /// <summary>
    /// The full screen mode selected by the player.
    /// 0 = Exclusive Fullscreen, 1 = Fullscreen Window, 2 = Windowed
    /// </summary>
    public int fullScreenMode;

    #endregion

    #region Audio

    [Header("Audio")]

    /// <summary>
    /// Array of volume levels.
    /// Each index corresponds to a specific channel (e.g., 0 = Master, 1 = Music, 2 = SFX).
    /// </summary>
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;

    #endregion



    #region Controls

    [Header("Controls")]

    /// <summary>
    /// Names of the control keys (e.g."Forward").
    /// Used to display key bindings in the UI.
    /// </summary>
    public string[] keyNames;

    /// <summary>
    /// The actual key values assigned to the actions (e.g., "W").
    /// </summary>
    public string[] keyValues;



    #endregion

    


}
