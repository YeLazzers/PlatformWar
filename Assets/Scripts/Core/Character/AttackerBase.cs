using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimator))]
public abstract class AttackerBase : MonoBehaviour
{
    [SerializeField] protected int Damage;
    [Min(0.1f)]
    [SerializeField] protected float AttackCD;
    [SerializeField] protected float AttackRange;
    [SerializeField] protected Vector3 WeaponPositionOffset;

    private Coroutine _waitForAttackCoroutine;

    protected WaitForSeconds WaitForAttack;
    protected CharacterAnimator Animator;

    public bool IsAttackAvailable { get; protected set; }

    protected void Awake()
    {
        Animator = GetComponent<CharacterAnimator>();

        WaitForAttack = new WaitForSeconds(AttackCD);

        AllowAtack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + WeaponPositionOffset, transform.position + new Vector3(AttackRange * transform.right.x, 0) + WeaponPositionOffset);
    }

    protected IEnumerator WaitAttackCD()
    {
        yield return WaitForAttack;
        AllowAtack();
    }

    public void DoDamage()
    {
        Vector3 attackDirection = transform.right;
        List<RaycastHit2D> raycastHits = Physics2D.RaycastAll(transform.position + WeaponPositionOffset, attackDirection, AttackRange).ToList();

        IEnumerable<Collider2D> colliders = raycastHits.Where(hit => hit.collider != null && hit.collider.gameObject != gameObject).Select(hit => hit.collider);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(Damage);
            }
            if (collider.TryGetComponent(out IPushable pushable))
            {
                pushable.Push(attackDirection);
            }
            if (collider.TryGetComponent(out IHitable hitable))
            {
                hitable.Hit();
            }
        }
        StartCoroutine(WaitAttackCD());
    }

    public void Attack()
    {
        if (IsAttackAvailable)
        {
            DisallowAtack();
            Animator.SetAttack();
        }
    }

    public void AllowAtack()
    {
        IsAttackAvailable = true;
    }

    public void DisallowAtack()
    {
        IsAttackAvailable = false;
    }

    public void ReloadAttack()
    {
        DisallowAtack();
        if (_waitForAttackCoroutine != null)
            StopCoroutine(_waitForAttackCoroutine);

        _waitForAttackCoroutine = StartCoroutine(WaitAttackCD());
    }
}