using UnityEngine;

[RequireComponent(typeof(AbilityView))]
public class AbilityRuntimePresenter : MonoBehaviour
{
    [SerializeField] private AbilityView _view;

    private AbilityRuntime _runtime;

    private void OnValidate()
    {
        _view ??= GetComponent<AbilityView>();
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

    public void Initialize(AbilityRuntime runtime)
    {
        _runtime = runtime;

        name = $"{nameof(AbilityView)} - {runtime.Ability.name}";

        _runtime.Activated += _view.ShowActive;
        _runtime.DurationChanged += _view.SetActiveTimer;
        _runtime.Deactivated += _view.HideActive;

        _runtime.CooldownStarted += _view.ShowCooldown;
        _runtime.CooldownChanged += _view.SetCooldownTimer;
        _runtime.CooldownEnded += _view.HideCooldown;
    }
}