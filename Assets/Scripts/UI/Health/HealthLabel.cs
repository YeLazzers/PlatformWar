using TMPro;
using UnityEngine;

public class HealthLabel : UIHealthBase
{
    [SerializeField] private TextMeshProUGUI _currentTMP;
    [SerializeField] private TextMeshProUGUI _maxTMP;

    public override void Set(int current, int max)
    {
        SetCurrent(current);
        SetMax(max);
    }

    public void SetCurrent(int current)
    {
        _currentTMP.text = current.ToString();
    }

    public void SetMax(int max)
    {
        _maxTMP.text = max.ToString();
    }
}