using UnityEngine;
using UnityEngine.UI;

public class QualityManager : MonoBehaviour
{
    [SerializeField] Dropdown _qualityDropdown;
    int _currentqualityIndex = 0;

    public int CurrentQualityIndex
    {
        set { _currentqualityIndex = value; }
        get { return _currentqualityIndex; }
    }

    public void ChangeQuality(int qualityIndex)
    {
        CurrentQualityIndex = qualityIndex;
        QualitySettings.SetQualityLevel(CurrentQualityIndex);
    }

    private void Start()
    {
        _qualityDropdown.value = CurrentQualityIndex;
        QualitySettings.SetQualityLevel(CurrentQualityIndex);
    }
}
