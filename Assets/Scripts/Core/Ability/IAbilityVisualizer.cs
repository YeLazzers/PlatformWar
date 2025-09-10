public interface IAbilityVisualizer
{
    public void Initialize(Ability ability);
    public void Show(AbilityRuntime runtime, Unit caster);
    public void Hide();
}