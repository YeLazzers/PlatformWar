using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimator))]
public abstract class CharacterAttackerBase : MonoBehaviour
{
    [SerializeField] protected int _damage;
    [Min(0.1f)]
    [SerializeField] protected float _attackCD;
    [SerializeField] protected float _attackRange;
    [SerializeField] protected Vector3 _weaponPositionOffset;

    protected bool _isAttackAvailable = true;
    protected WaitForSeconds _waitForAttack;
    protected CharacterAnimator _characterAnimator;

    public bool IsAttackAvailable => _isAttackAvailable;

    protected void Awake()
    {
        _characterAnimator = GetComponent<CharacterAnimator>();

        _waitForAttack = new WaitForSeconds(_attackCD);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + _weaponPositionOffset, transform.position + new Vector3(_attackRange * transform.right.x, 0) + _weaponPositionOffset);
    }

    protected IEnumerator WaitAttackCD()
    {
        yield return _waitForAttack;
        AllowAtack();
    }

    public void DoDamage()
    {
        RaycastHit2D[] raycastHits = Physics2D.RaycastAll(transform.position + _weaponPositionOffset, transform.right, _attackRange);

        foreach (var raycastHit in raycastHits)
        {
            if (raycastHit.collider
                && raycastHit.collider.gameObject != gameObject
                && raycastHit.collider.TryGetComponent(out IDamageable target))
            {
                target.TakeDamage(_damage, transform);
            }
        }
        StartCoroutine(WaitAttackCD());
    }

    public void Attack()
    {
        if (_isAttackAvailable)
        {
            DisallowAtack();
            _characterAnimator.SetAttack();
        }
    }

    public void AllowAtack() => _isAttackAvailable = true;

    public void DisallowAtack() => _isAttackAvailable = false;

    public void ReloadAttack()
    {
        DisallowAtack();
        StartCoroutine(WaitAttackCD());
    }
}