using System.Linq;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private AbilityLibrary _library;
    [SerializeField] private VFXHolder _VFXHolder;
    [SerializeField] private AbilityPanelView _abilityPanel;

    public void Initialize(Player player)
    {
        var abilityCaster = player.GetComponent<PlayerAbilityCaster>();
        abilityCaster.Initialize(_library, _VFXHolder.transform);

        _abilityPanel.Initialize(abilityCaster.Abilities.Values.ToList());
    }
}