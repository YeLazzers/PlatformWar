using System;
using System.Collections;
using UnityEngine;

public class AbilityRuntime
{
    private Ability _ability;
    private IAbilityVisualizer _visualizer;
    private WaitForSeconds _tickWaiter;

    public AbilityRuntime(Ability ability, AbilityVisualizerBase visualizer)
    {
        _ability = ability;
        _visualizer = visualizer;

        _tickWaiter = new WaitForSeconds(_ability.TickRate);

        IsAvailable = true;
    }

    public event Action<float> Activated;
    public event Action<float, float> DurationChanged;
    public event Action Deactivated;
    public event Action<float> CooldownStarted;
    public event Action<float, float> CooldownChanged;
    public event Action CooldownEnded;

    public Ability Ability => _ability;
    public float DurationTimer { get; private set; }
    public float CooldownTimer { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsAvailable { get; private set; }

    public IEnumerator Execute(Unit caster)
    {
        if (IsActive) yield break;

        IsActive = true;
        IsAvailable = false;


        switch (_ability.ExecutionPolicy)
        {
            case AbilityExecutionPolicy.Instant:
                yield return ExecuteOnce(caster);
                IsActive = false;
                yield return WaitWhileCooldown();
                break;

            case AbilityExecutionPolicy.Channeled:
                yield return ExecuteChanneled(caster);
                IsActive = false;
                yield return WaitWhileCooldown();
                break;

            case AbilityExecutionPolicy.Aura:
                yield return ExecuteAura(caster);
                break;
        }

        IsAvailable = true;
    }

    private IEnumerator ExecuteOnce(Unit caster)
    {
        ApplyActions(caster);
        yield break;
    }

    private IEnumerator ExecuteChanneled(Unit caster)
    {
        DurationTimer = _ability.Duration;

        Activated?.Invoke(_ability.Duration);
        _visualizer.Show(caster, this);

        while (DurationTimer > 0)
        {
            ApplyActions(caster);
            yield return _tickWaiter;
            DurationTimer -= _ability.TickRate;

            DurationChanged?.Invoke(DurationTimer, _ability.Duration);
        }

        _visualizer.Hide();
        Deactivated?.Invoke();
    }
    private IEnumerator ExecuteAura(Unit caster)
    {
        Activated?.Invoke(0f);

        while (IsActive)
        {
            ApplyActions(caster);
            yield return _tickWaiter;
        }

        Deactivated?.Invoke();
    }

    private IEnumerator WaitWhileCooldown()
    {
        CooldownTimer = _ability.Cooldown;

        CooldownStarted?.Invoke(_ability.Cooldown);

        while (CooldownTimer > 0)
        {
            yield return _tickWaiter;
            CooldownTimer -= _ability.TickRate;

            CooldownChanged?.Invoke(CooldownTimer, _ability.Cooldown);
        }

        CooldownEnded?.Invoke();
    }

    private void ApplyActions(Unit caster)
    {

        var context = new AbilityContext
        {
            Caster = caster,
            Targets = null,
            Data = new(),
        };

        context.Targets = _ability.SelectTargets(context);
        _visualizer.UpdateContext(context);

        if (context.Targets != null)
        {
            _ability.ApplyActions(context);
        }
    }
}