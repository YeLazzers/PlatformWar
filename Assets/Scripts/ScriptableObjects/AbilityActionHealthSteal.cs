using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "AAHealthSteal", menuName = "Abilities/Actions/HealthSteal")]
public class AbilityActionHealthSteal : AbilityActionBase
{
    [SerializeField] private AbilityActionDamage _actionDamage;
    [SerializeField] private AbilityActionHeal _actionHeal;

    public override AbilityResult ApplyAction(AbilityContext context)
    {
        AbilityResult result = _actionDamage.ApplyAction(context);

        float totalHeal = result.Events.Where(x => x.Type == EffectType.Damage).Sum(x => x.Value);
        context.Data.Add(AbilityContextDataKeys.Heal, totalHeal);

        context.Targets = new() { context.Caster };

        result.Events.Concat(_actionHeal.ApplyAction(context).Events);

        return result;
    }
}