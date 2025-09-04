using System.Collections.Generic;

public enum EffectType { Damage, Heal }

public class EffectEvent
{
    public Unit Target;
    public float Value;     // фактически применённое значение
    public EffectType Type;
}

public class AbilityResult
{
    public List<EffectEvent> Events;

    public AbilityResult()
    {
        Events = new();
    }
}