using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private Animator _animator;

    private readonly int _isRunningHash = Animator.StringToHash("IsRunning");
    private readonly int _isGroundedHash = Animator.StringToHash("IsGrounded");
    private readonly int _isFlyingHash = Animator.StringToHash("IsFlying");
    private readonly int _isJumpingHash = Animator.StringToHash("IsJumping");
    private readonly int _attackHash = Animator.StringToHash("Attack");
    private readonly int[] _attackHashes = new int[] { Animator.StringToHash("Attack1"), Animator.StringToHash("Attack2") };
    private readonly int _hitHash = Animator.StringToHash("Hit");
    private readonly int _dieHash = Animator.StringToHash("Die");

    public void Awake() => _animator = GetComponent<Animator>();
    public void SetIsRunning(bool value) => _animator.SetBool(_isRunningHash, value);

    public void SetIsGrounded(bool value)
    {
        _animator.SetBool(_isGroundedHash, value);

        if (value)
        {
            SetIsJumping(false);
            SetIsFlying(false);
        }
    }

    public void SetIsFlying(bool value)
    {
        _animator.SetBool(_isFlyingHash, value);

        if (value)
        {
            SetIsJumping(false);
        }
    }

    public void SetIsJumping(bool value)
    {
        _animator.SetBool(_isJumpingHash, value);

        if (value)
        {
            SetIsFlying(false);
        }
    }

    public void SetAttack() => _animator.SetTrigger(_attackHash);

    public void SetRandomAttack() => _animator.SetTrigger(_attackHashes[Random.Range(0, _attackHashes.Length)]);

    public void SetHit() => _animator.SetTrigger(_hitHash);

    public void SetDie() => _animator.SetTrigger(_dieHash);
}