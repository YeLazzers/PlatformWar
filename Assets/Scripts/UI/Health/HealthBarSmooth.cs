using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarSmooth : UIHealthBase
{
    [SerializeField] private float _smoothTime;

    private Slider _slider;
    private float _currentValue;
    private float _maxValue;
    private Coroutine _coroutine;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public override void Set(float current, float max)
    {
        _maxValue = max;
        _slider.maxValue = _maxValue;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SetSmooth(_currentValue, current));
    }

    private IEnumerator SetSmooth(float start, float target)
    {
        while (Mathf.Approximately(_currentValue, target) == false)
        {
            _currentValue = _smoothTime == 0 ? target : Mathf.MoveTowards(_currentValue, target, Mathf.Abs(start - target) * Time.deltaTime / _smoothTime);
            _slider.value = _currentValue;
            yield return null;
        }
    }
}