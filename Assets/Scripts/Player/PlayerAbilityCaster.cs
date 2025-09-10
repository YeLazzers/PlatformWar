using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class PlayerAbilityCaster : MonoBehaviour
{
    [SerializeField] private Unit _caster;
    [SerializeField] private AbilityNames _primary;

    private InputReader _inputReader;
    public Dictionary<AbilitySlot, Ability> Abilities { get; private set; }

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _inputReader.AbilityPressed += OnAbilityTriggered;
    }

    private void OnDisable()
    {
        _inputReader.AbilityPressed -= OnAbilityTriggered;
    }

    public void Initialize(AbilityLibrary library, Transform vfxHolder)
    {
        Abilities = new()
        {
            { AbilitySlot.Primary, InitializeAbility(library, _primary, vfxHolder) }
        };
    }

    private void OnAbilityTriggered(AbilitySlot slot)
    {
        Abilities.TryGetValue(slot, out Ability ability);

        if (ability != null && ability.Runtime.IsAvailable)
        {
            ability.Execute();
        }
    }

    private Ability InitializeAbility(AbilityLibrary library, AbilityNames name, Transform vfxHolder)
    {
        AbilityConfig config = library.GetAbilityConfig(name);
        Ability ability = new(config, _caster);

        return ability;
    }
}