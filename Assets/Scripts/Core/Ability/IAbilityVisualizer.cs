public interface IAbilityVisualizer
{
    public void Initialize(Ability ability);
    public void Show(Unit caster, AbilityRuntime runtime);
    public void UpdateContext(AbilityContext context);
    public void Hide();
}