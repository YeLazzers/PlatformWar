using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityTargetSelectorBase : ScriptableObject
{
    public abstract List<Unit> TryGetTargets(AbilityContext context);
}