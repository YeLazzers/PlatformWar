using UnityEngine;
using UnityEngine.UI;

public class HealthBar : UIHealthBase
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public override void Set(int current, int max)
    {

        _slider.maxValue = max;
        _slider.value = current;
    }
}