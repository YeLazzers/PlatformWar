using UnityEngine;

[RequireComponent(typeof(AbilityButtonView))]
public class AbilityRuntimePresenter : MonoBehaviour
{
    [SerializeField] private AbilityButtonView _view;

    private AbilityRuntime _runtime;

    private void Awake()
    {
        _view = GetComponent<AbilityButtonView>();
    }

    private void OnDisable()
    {

        _runtime.Activated -= _view.ShowActive;
        _runtime.DurationChanged -= _view.SetActiveTimer;
        _runtime.Deactivated -= _view.HideActive;

        _runtime.CooldownStarted -= _view.ShowCooldown;
        _runtime.CooldownChanged -= _view.SetCooldownTimer;
        _runtime.CooldownEnded -= _view.HideCooldown;
    }

    public void Initialize(AbilityRuntime runtime, AbilityConfig config)
    {
        _runtime = runtime;

        name = $"{nameof(AbilityButtonView)} - {config.Name}";

        _view.SetIcon(config.Icon);

        _runtime.Activated += _view.ShowActive;
        _runtime.DurationChanged += _view.SetActiveTimer;
        _runtime.Deactivated += _view.HideActive;

        _runtime.CooldownStarted += _view.ShowCooldown;
        _runtime.CooldownChanged += _view.SetCooldownTimer;
        _runtime.CooldownEnded += _view.HideCooldown;
    }
}