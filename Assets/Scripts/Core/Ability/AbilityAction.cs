using System.Collections.Generic;
using System.Linq;

public static class AbilityAction
{
    public static AbilityResult ApplyAction(AbilityConfig ability, AbilityContext context)
    {
        AbilityResult result = new();

        switch (ability.Name)
        {
            case AbilityNames.HealthSteal:
                {
                    result.Events = ActionHealthSteal(context);
                    break;
                }
        }

        return result;
    }

    private static List<EffectEvent> ActionDamage(AbilityContext context)
    {
        List<EffectEvent> events = new();

        float damage = (float)context.Data.GetValueOrDefault(AbilityContextDataKeys.Damage);

        return context.Targets.Select(target =>
            {
                IDamageable damageable = target.GetComponentInChildren<IDamageable>();

                return new EffectEvent()
                {
                    Target = target,
                    Value = damageable.TakeDamage(damage),
                    Type = EffectType.Damage
                };
            }).ToList();
    }

    private static List<EffectEvent> ActionHeal(AbilityContext context)
    {
        float heal = (float)context.Data.GetValueOrDefault(AbilityContextDataKeys.Heal);

        return context.Targets.Select(target =>
            {
                IHealable healable = target.GetComponentInChildren<IHealable>();

                return new EffectEvent()
                {
                    Target = target,
                    Value = healable.TakeHeal(heal),
                    Type = EffectType.Damage
                };
            }).ToList();
    }


    private static List<EffectEvent> ActionHealthSteal(AbilityContext context)
    {
        var damageEvents = ActionDamage(context);

        context.Data[AbilityContextDataKeys.Heal] = damageEvents.Sum(x => x.Value);

        context.Targets = new() { context.Caster };

        return damageEvents.Concat(ActionHeal(context)).ToList();
    }
}