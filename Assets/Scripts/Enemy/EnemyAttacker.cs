using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyAttacker: CharacterAttackerBase
{
    public IEnumerator Attacking(Transform target, Enemy enemy)
    {
        enemy.StopMoving();
        while (Vector2.Distance(transform.position, target.position) < _attackRange)
        {
            if (_isAttackAvailable)
            {
                Attack();
            }
            yield return null;
        }
        enemy.ContinueMoving();
    }
}