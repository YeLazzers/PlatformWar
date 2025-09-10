using System.Collections;
using UnityEngine;

public class ExecutorChanneled : IAbilityExecutable
{
    public IEnumerator Execute(AbilityRuntime runtime, AbilityContext context)
    {
        AbilityConfig config = runtime.AbilityConfig;
        float durationTimer = config.Duration;
        WaitForSeconds tickWaiter = new WaitForSeconds(config.TickRate);

        runtime.NotifyActivated(durationTimer);

        while (durationTimer > 0)
        {
            context.Data[AbilityContextDataKeys.Point] = context.Caster.transform.position;
            context.Data[AbilityContextDataKeys.Damage] = config.DamagePerSecond / (1f / config.TickRate);

            runtime.ApplyActions(context);

            yield return tickWaiter;

            durationTimer -= config.TickRate;
            runtime.NotifyDurationChanged(durationTimer, config.Duration);
        }

        runtime.NotifyDeactivated();
    }
}