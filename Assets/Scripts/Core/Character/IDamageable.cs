using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(int amount, Transform attacker);
}