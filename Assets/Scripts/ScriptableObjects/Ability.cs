using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/New Ability")]
public class Ability : ScriptableObject
{
    [Header("SO")]
    [SerializeField] private AbilityActionBase _action;
    [SerializeField] private AbilityTargetSelectorBase _targetSelector;
    [SerializeField] private AbilityVisualizerBase _visualizer;

    [Header("Config")]
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;

    [Header("Params")]
    [SerializeField] private int _damage;
    [SerializeField] private int _damagePerSecond;

    [Header("Timing")]
    [SerializeField] private AbilityExecutionPolicy _executionPolicy;
    [SerializeField] private float _duration;
    [SerializeField] private float _tickRate;
    [SerializeField] private float _cooldown;

    [Header("Targeting")]
    [SerializeField] private AbilityTargetingPolicy _targetingPolicy;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layerMask;

    public AbilityTargetingPolicy TargetingPolicy => _targetingPolicy;
    public AbilityExecutionPolicy ExecutionPolicy => _executionPolicy;
    public AbilityVisualizerBase Visualizer => _visualizer;
    public float Duration => _duration;
    public float TickRate => _tickRate;
    public float Cooldown => _cooldown;
    public float Radius => _radius;
    public Sprite Icon => _icon;
    public string Name => _name;

    public void ApplyActions(AbilityContext context)
    {
        switch (_executionPolicy)
        {
            case AbilityExecutionPolicy.Channeled:
                {
                    float damagePerTick = _damagePerSecond / (1f / _tickRate);
                    context.Data.Add(AbilityContextDataKeys.Damage, damagePerTick);
                    break;
                }
        }

        _action.ApplyAction(context);
    }

    public List<Unit> SelectTargets(AbilityContext context)
    {
        switch (_targetingPolicy)
        {
            case AbilityTargetingPolicy.Self:
                {
                    context.Data.Add(AbilityContextDataKeys.Point, context.Caster.transform.position);
                    break;
                }
        }
        context.Data.Add(AbilityContextDataKeys.Radius, _radius);
        context.Data.Add(AbilityContextDataKeys.LayerMask, _layerMask);
        return _targetSelector.TryGetTargets(context);
    }
}