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

    protected WaitForSeconds WaitForAttack;
    protected CharacterAnimator Animator;


    private Coroutine _waitForAttackCoroutine;

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

            if (collider.TryGetComponent(out IHitable hitable))
            {
                hitable.Hit();
            }

            if (collider.TryGetComponent(out IKnockbackable knockbackable))
            {
                knockbackable.Knockback(attackDirection);
            }
        }

        ReloadAttack();
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

    private IEnumerator WaitAttackCD()
    {
        yield return WaitForAttack;
        AllowAtack();
    }
}