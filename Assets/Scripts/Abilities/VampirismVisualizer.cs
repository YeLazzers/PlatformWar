using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class VampirismVisualizer : AbilityVisualizerBase
{
    private float radiusScaler = 2;

    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _targetedColor;

    private SpriteRenderer _renderer;

    public override void Initialize(Ability ability)
    {
        _renderer = GetComponent<SpriteRenderer>();

        transform.localScale = new Vector2(ability.Radius * radiusScaler, ability.Radius * radiusScaler);

        Hide();
    }

    public override void Hide()
    {
        _renderer.enabled = false;
    }

    public override void Show(Unit caster, AbilityRuntime runtime)
    {
        _renderer.enabled = true;
    }

    public override void UpdateContext(AbilityContext context)
    {
        _renderer.color = context.Targets != null && context.Targets.Count > 0 ? _targetedColor : _defaultColor;
    }
}