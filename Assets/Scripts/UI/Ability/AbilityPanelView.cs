using System.Collections.Generic;
using UnityEngine;

public class AbilityPanelView : MonoBehaviour
{
    [SerializeField] private AbilityRuntimePresenter _presenterPrefab;

    public void Initialize(List<Ability> abilities)
    {
        abilities.ForEach(ability =>
        {
            var runtimePresenter = Instantiate(_presenterPrefab, transform);
            runtimePresenter.Initialize(ability.Runtime, ability.Config);
        });
    }
}