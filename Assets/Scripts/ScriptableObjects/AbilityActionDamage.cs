using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "AADamage", menuName = "Abilities/Actions/Damage")]
public class AbilityActionDamage : AbilityActionBase
{
    public override AbilityResult ApplyAction(AbilityContext context)
    {
        AbilityResult result = new();

        float damage = (float)context.Data.GetValueOrDefault(AbilityContextDataKeys.Damage);
        context.Targets
            .ForEach(target =>
            {
                IDamageable damageable = target.GetComponentInChildren<IDamageable>();

                result.Events.Add(new EffectEvent()
                {
                    Target = target,
                    Value = damageable.TakeDamage(damage),
                    Type = EffectType.Damage
                });
            });

        return result;
    }
}