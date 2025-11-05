using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AutioManager : MonoBehaviour
{
    [Header("Audio Mixer")]
    public AudioMixer audioMixer; 
    [Header("Volume Control Sliders")]
    [SerializeField] Slider[] _slider = new Slider[3];
    [SerializeField] Text[] _percentText = new Text[3];
    [SerializeField] public float[] _volume = new float[3];
    [SerializeField] string[] _channelName = new string[3];

    public float[] VolumeControl
    {
        set
        {
            _volume = value;
        }
        get
        {
            return _volume;
        }
    }

    private void Start()
    {
        for (int i = 0; i < _volume.Length; i++)
        {
            _slider[i].value = VolumeControl[i];
            audioMixer.SetFloat(_channelName[i], VolumeControl[i]);
            _percentText[i].text = $"{Mathf.Clamp01((VolumeControl[i] + 80) / 100):P0}";
        }
    }

    public void ChangeVolume(int volumeID)
    {
        VolumeControl[volumeID] = _slider[volumeID].value;
        audioMixer.SetFloat(_channelName[volumeID], VolumeControl[volumeID]);
        _percentText[volumeID].text = $"{Mathf.Clamp01((VolumeControl[volumeID] + 80) / 100):P0}";
    }
}

