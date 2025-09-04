using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AbilityView : MonoBehaviour
{
    private readonly float _roundLimit = 1f;
    private readonly int _roundDigits = 1;

    [SerializeField] private Image _cooldownRect;
    [SerializeField] private TextMeshProUGUI _cooldownTimerTMP;
    [SerializeField] private Image _activeRect;
    [SerializeField] private TextMeshProUGUI _activeTimerTMP;
    [SerializeField] private TextMeshProUGUI _inputKeyTMP;
    [SerializeField] private Image _icon;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void SetCooldownTimer(float current, float maxValue)
    {
        float roundedValue = current > _roundLimit ? Mathf.Ceil(current) : (float)Math.Round((decimal)current, _roundDigits);

        _cooldownTimerTMP.text = roundedValue.ToString();
    }


    public void ShowActive(float duration)
    {
        _activeRect.gameObject.SetActive(true);

        if (duration > 0)
            SetActiveTimer(duration, duration);
    }

    public void SetActiveTimer(float current, float maxValue)
    {
        _slider.maxValue = maxValue;
        _slider.value = current;
    }

    public void HideActive()
    {
        _activeRect.gameObject.SetActive(false);
    }

    public void ShowCooldown(float cooldown)
    {
        _cooldownRect.gameObject.SetActive(true);
        SetCooldownTimer(cooldown, cooldown);
    }

    public void HideCooldown()
    {
        _cooldownRect.gameObject.SetActive(false);
    }
}