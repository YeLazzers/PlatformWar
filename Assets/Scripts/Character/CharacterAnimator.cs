using UnityEngine;

[RequireComponent (typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private Animator _animator;

    private readonly int _isRunningHash = Animator.StringToHash("IsRunning");
    private readonly int _isGroundedHash = Animator.StringToHash("IsGrounded");
    private readonly int _isFlyingHash = Animator.StringToHash("IsFlying");
    private readonly int _isJumping = Animator.StringToHash("IsJumping");

    public void Awake()
    {
        _animator = GetComponent<Animator>();
    }

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
        _animator.SetBool(_isJumping, value);

        if (value)
        {
            SetIsFlying(false);
        }
    }
}