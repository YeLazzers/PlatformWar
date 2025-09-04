using System;
using System.Collections.Generic;

[Serializable]
public enum AbilityContextDataKeys
{
    Damage,
    Heal,
    Radius,
    Point,
    LayerMask,
}

public class AbilityContext
{
    public Unit Caster;
    public List<Unit> Targets;
    public Dictionary<AbilityContextDataKeys, object> Data;
}