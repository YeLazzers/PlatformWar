using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/New Ability Config")]
public class AbilityConfig : ScriptableObject
{
    [Header("Base")]
    [SerializeField] private AbilityCastingPolicy _castPositionPolicy;
    [SerializeField] private AbilityExecutionPolicy _executionPolicy;
    [SerializeField] private AbilityTargetingPolicy _targetingPolicy;
    [SerializeField] private AbilityVisualizerBase _visualizerPrefab;

    [Header("Params")]
    [SerializeField] private AbilityNames _name;
    [SerializeField] private Sprite _icon;

    [Header("Attributes")]
    [SerializeField] private int _damage;
    [SerializeField] private int _damagePerSecond;

    [Header("Timing")]
    [SerializeField] private float _duration;
    [SerializeField] private float _tickRate;
    [SerializeField] private float _cooldown;

    [Header("Targeting")]
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layerMask;

    // Base    
    public AbilityCastingPolicy CastPositionPolicy => _castPositionPolicy;
    public AbilityExecutionPolicy ExecutionPolicy => _executionPolicy;
    public AbilityTargetingPolicy TargetingPolicy => _targetingPolicy;
    public AbilityVisualizerBase VisualizerPrefab => _visualizerPrefab;

    // Params
    public Sprite Icon => _icon;
    public AbilityNames Name => _name;

    // Attributes
    public float Damage => _damage;
    public float DamagePerSecond => _damagePerSecond;

    // Timing
    public float Duration => _duration;
    public float TickRate => _tickRate;
    public float Cooldown => _cooldown;

    // Targeting
    public float Radius => _radius;
    public LayerMask LayerMask => _layerMask;
}