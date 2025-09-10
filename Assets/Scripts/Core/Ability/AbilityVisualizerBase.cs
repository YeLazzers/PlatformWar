using UnityEngine;

public abstract class AbilityVisualizerBase : MonoBehaviour, IAbilityVisualizer
{
    public abstract void Initialize(Ability ability);

    public abstract void Hide();

    public abstract void Show(AbilityRuntime runtime, Unit caster);
}