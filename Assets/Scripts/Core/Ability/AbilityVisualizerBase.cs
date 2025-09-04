using UnityEngine;

public abstract class AbilityVisualizerBase : MonoBehaviour, IAbilityVisualizer
{
    public abstract void Initialize(Ability ability);

    public abstract void Hide();

    public abstract void Show(Unit caster, AbilityRuntime runtime);

    public abstract void UpdateContext(AbilityContext context);
}