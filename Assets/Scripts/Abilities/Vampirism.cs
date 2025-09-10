using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Vampirism : AbilityVisualizerBase
{
    private readonly float radiusScaler = 2;

    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _targetedColor;

    private SpriteRenderer _renderer;
    private AbilityContext _context;

    private void Update()
    {
        transform.position = _context.Caster.transform.position;


        _renderer.color = _context.Targets != null && _context.Targets.Count > 0 ? _targetedColor : _defaultColor;
    }

    public override void Initialize(Ability ability)
    {
        _renderer = GetComponent<SpriteRenderer>();
        _context = ability.Context;

        transform.localScale = new Vector2(ability.Config.Radius * radiusScaler, ability.Config.Radius * radiusScaler);

        Hide();
    }

    public override void Hide()
    {
        _renderer.enabled = false;
    }

    public override void Show(AbilityRuntime runtime, Unit caster)
    {
        _renderer.enabled = true;
    }
}