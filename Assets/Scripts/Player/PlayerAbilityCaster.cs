using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class PlayerAbilityCaster : Unit
{
    [SerializeField] private Ability _primarySO;

    private InputReader _inputReader;
    public Dictionary<AbilitySlot, AbilityRuntime> AbilityRuntimes { get; private set; }

    private void OnValidate()
    {
        _inputReader ??= GetComponent<InputReader>();
        AbilityRuntimes ??= new();
    }

    private void Awake()
    {
        InitializeAbility(AbilitySlot.Primary, _primarySO);
    }

    private void OnEnable()
    {
        _inputReader.AbilityPressed += OnAbilityTriggered;
    }

    private void OnDisable()
    {
        _inputReader.AbilityPressed -= OnAbilityTriggered;
    }

    private void OnAbilityTriggered(AbilitySlot slot)
    {
        AbilityRuntimes.TryGetValue(slot, out AbilityRuntime ability);

        if (ability != null && ability.IsAvailable)
        {
            StartCoroutine(ability.Execute(this));
        }
    }

    private void InitializeAbility(AbilitySlot slot, Ability ability)
    {
        AbilityVisualizerBase visualizer = Instantiate(ability.Visualizer, transform);
        visualizer.Initialize(ability);

        AbilityRuntime runtime = new AbilityRuntime(ability, visualizer);

        AbilityRuntimes.Add(slot, runtime);
    }
}