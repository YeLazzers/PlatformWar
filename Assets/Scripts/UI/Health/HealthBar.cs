using UnityEngine;
using UnityEngine.UI;

public class HealthBar : UIHealthBase
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public override void Set(float current, float max)
    {

        _slider.maxValue = max;
        _slider.value = current;
    }
}