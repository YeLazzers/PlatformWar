public static class ExecutorFaсtory
{
    public static IAbilityExecutable InstantiateExecutor(AbilityExecutionPolicy type)
    {
        switch (type)
        {
            case AbilityExecutionPolicy.Instant:
                return new ExecutorInstant();
            case AbilityExecutionPolicy.Channeled:
                return new ExecutorChanneled();
        }

        return null;
    }
}