using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AAHeal", menuName = "Abilities/Actions/Heal")]
public class AbilityActionHeal : AbilityActionBase
{
    public override AbilityResult ApplyAction(AbilityContext context)
    {

        AbilityResult result = new();

        float heal = (float)context.Data.GetValueOrDefault(AbilityContextDataKeys.Heal);

        context.Targets
            .ForEach(target =>
            {
                IHealable healable = target.GetComponentInChildren<IHealable>();

                result.Events.Add(new EffectEvent()
                {
                    Target = target,
                    Value = healable.TakeHeal(heal),
                    Type = EffectType.Heal
                });
            });

        return result;
    }
}