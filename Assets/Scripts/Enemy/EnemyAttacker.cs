using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyAttacker: AttackerBase
{
    private Coroutine _attackingCoroutine;

    private IEnumerator Attacking(Transform target)
    {
        while (transform.position.IsEnoughClose(target.position, AttackRange))
        {
            if (IsAttackAvailable)
            {
                Attack();
            }
            yield return null;
        }
    }

    public void StartAttacking(Transform target)
    {
        StopAttacking();

        _attackingCoroutine = StartCoroutine(Attacking(target));
    }

    public void StopAttacking()
    {
        if (_attackingCoroutine != null)
            StopCoroutine(_attackingCoroutine);
    }
}