using System;
using System.Collections;
using UnityEngine;

public class AbilityRuntime
{
    private AbilityConfig _config;
    private IAbilityExecutable _executor;
    private WaitForSeconds _tickWaiter;

    public AbilityRuntime(AbilityConfig config)
    {
        _config = config;

        _tickWaiter = new WaitForSeconds(config.TickRate);
        _executor = ExecutorFaсtory.InstantiateExecutor(config.ExecutionPolicy);

        IsAvailable = true;
    }

    public event Action<float> Activated;
    public event Action<float, float> DurationChanged;
    public event Action Deactivated;
    public event Action<float> CooldownStarted;
    public event Action<float, float> CooldownChanged;
    public event Action CooldownEnded;

    public AbilityConfig AbilityConfig => _config;
    public float DurationTimer { get; private set; }
    public float CooldownTimer { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsAvailable { get; private set; }

    public IEnumerator Execute(AbilityContext context, IAbilityVisualizer visualizer)
    {
        if (IsActive) yield break;

        IsAvailable = false;

        IsActive = true;
        visualizer.Show(this, context.Caster);

        yield return _executor.Execute(this, context);

        IsActive = false;
        visualizer.Hide();

        yield return WaitWhileCooldown();

        IsAvailable = true;
    }

    public void ApplyActions(AbilityContext context)
    {

        context.Targets = AbilityTargetSelector.TryGetTargets(_config, context);

        if (context.Targets != null)
        {
            AbilityAction.ApplyAction(_config, context);
        }
    }

    public void NotifyActivated(float duration)
    {
        DurationTimer = duration;
        Activated?.Invoke(duration);
    }

    public void NotifyDurationChanged(float current, float max)
    {
        DurationTimer = current;
        DurationChanged?.Invoke(current, max);
    }

    public void NotifyDeactivated()
    {
        Deactivated?.Invoke();
    }

    private IEnumerator WaitWhileCooldown()
    {
        CooldownTimer = _config.Cooldown;

        CooldownStarted?.Invoke(_config.Cooldown);

        while (CooldownTimer > 0)
        {
            yield return null;
            CooldownTimer -= Time.deltaTime;

            CooldownChanged?.Invoke(CooldownTimer, _config.Cooldown);
        }

        CooldownEnded?.Invoke();
    }
}