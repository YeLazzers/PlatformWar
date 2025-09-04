using UnityEngine;

public class AbilityPanelView : MonoBehaviour
{
    [SerializeField] private PlayerAbilityCaster _abilityCaster;
    [SerializeField] private AbilityRuntimePresenter _abilityPresenterPrefab;

    private void Start()
    {
        foreach (var abilityRuntime in _abilityCaster.AbilityRuntimes.Values)
        {
            var abilityPresenter = Instantiate(_abilityPresenterPrefab, transform);
            abilityPresenter.Initialize(abilityRuntime);
        }
    }
}