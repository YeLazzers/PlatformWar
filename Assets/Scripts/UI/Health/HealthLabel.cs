using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HealthLabel : UIHealthBase
{
    [SerializeField] private TextMeshProUGUI _currentTMP;
    [SerializeField] private TextMeshProUGUI _maxTMP;

    public override void Set(float current, float max)
    {
        SetCurrent(current);
        SetMax(max);
    }

    public void SetCurrent(float current)
    {
        _currentTMP.text = current.ConvertTo<int>().ToString();
    }

    public void SetMax(float max)
    {
        _maxTMP.text = max.ConvertTo<int>().ToString();
    }
}