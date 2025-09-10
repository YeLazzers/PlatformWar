using System.Collections;

public interface IAbilityExecutable
{
    public IEnumerator Execute(AbilityRuntime runtime, AbilityContext context);
    // public void Execute(AbilityRuntime runtime);
    // public void Execute(AbilityRuntime runtime, Func<AbilityResult> func);
}