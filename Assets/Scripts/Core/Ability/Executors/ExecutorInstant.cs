using System.Collections;

public class ExecutorInstant : IAbilityExecutable
{
    public IEnumerator Execute(AbilityRuntime runtime, AbilityContext context)
    {
        runtime.ApplyActions(context);
        yield return null;
    }
}