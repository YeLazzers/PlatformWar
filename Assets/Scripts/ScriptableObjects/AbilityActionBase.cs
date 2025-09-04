using System.Collections.Generic;
using UnityEngine;


public abstract class AbilityActionBase : ScriptableObject
{
    public abstract AbilityResult ApplyAction(AbilityContext context);

    public void ApplyAction(List<AbilityContext> contexts)
    {
        foreach (AbilityContext context in contexts)
        {
            ApplyAction(context);
        }
    }
}