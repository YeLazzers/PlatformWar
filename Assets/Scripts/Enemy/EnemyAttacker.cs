using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyAttacker: CharacterAttackerBase
{
    private Enemy _enemy;

    private new void Awake()
    {
        base.Awake();
        _enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        StartCoroutine(Attacking());
    }

    private IEnumerator Attacking()
    {
        while (enabled)
        {
            yield return _waitForAttack;
            _enemy.PatrolStop();
            _characterAnimator.SetAttack();
        }
    }
}