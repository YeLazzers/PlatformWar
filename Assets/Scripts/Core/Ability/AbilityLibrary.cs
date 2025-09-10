using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityLibrary : MonoBehaviour
{
    [SerializeField] private string _configsPath;

    private List<AbilityConfig> _abilityConfigs;

    private void Awake()
    {
        _abilityConfigs = Resources.LoadAll(_configsPath).Select(x => (AbilityConfig)x).ToList();
    }

    public AbilityConfig GetAbilityConfig(AbilityNames abilityName)
    {
        return _abilityConfigs.Where(x => x.Name == abilityName).FirstOrDefault();
    }
}