using UnityEngine;
using UnityEngine.UI;

public class QualityManager : MonoBehaviour
{
    [SerializeField] Dropdown _qualityDropdown;
    int currentqualityIndex = 0;

    public int CurrentQualityIndex
    {
        set { currentqualityIndex = value; }
        get { return currentqualityIndex; }
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
