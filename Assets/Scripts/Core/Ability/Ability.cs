using UnityEngine;

public class Ability
{
    private Unit _caster;
    private AbilityConfig _config;
    private AbilityContext _context;
    private AbilityRuntime _runtime;
    private IAbilityVisualizer _visualizer;
    private Coroutine _executeCoroutine;

    public Ability(AbilityConfig config, Unit caster, Transform parent)
    {
        _config = config;
        _caster = caster;

        _context = new AbilityContext
        {
            Caster = caster,
            Data = new(),
            Targets = null,
        };
        FillDefaultContext();

        _visualizer = GameObject.Instantiate(config.VisualizerPrefab, parent);
        _visualizer.Initialize(this);


        _runtime = new AbilityRuntime(_config);
    }

    public AbilityConfig Config => _config;
    public AbilityRuntime Runtime => _runtime;
    public AbilityContext Context => _context;

    public void Execute()
    {
        _executeCoroutine = _caster.StartCoroutine(_runtime.Execute(_context, _visualizer));
    }

    private void FillDefaultContext()
    {
        _context.Data.Add(AbilityContextDataKeys.Radius, _config.Radius);
        _context.Data.Add(AbilityContextDataKeys.LayerMask, _config.LayerMask);
    }
}