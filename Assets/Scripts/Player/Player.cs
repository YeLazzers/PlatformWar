using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Wallet))]
[RequireComponent(typeof(PlayerAttacker))]
[RequireComponent(typeof(CharacterAnimationEvents))]
public class Player : MonoBehaviour, IHitable
{
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _movementSpeed;

    private Rigidbody2D _rigidbody;
    private CharacterAnimator _animator;
    private CharacterAnimationEvents _animationEvents;
    private Health _health;
    private Wallet _wallet;
    private PlayerAttacker _attacker;
    private DirectionFlipper2D _directionFlipper;
    private bool _isMoving;

    public event Action<Player> Dead;

    public Health Health => _health;
    public Wallet Wallet => _wallet;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<CharacterAnimator>();
        _animationEvents = GetComponent<CharacterAnimationEvents>();
        _health = GetComponent<Health>();
        _wallet = GetComponent<Wallet>();
        _attacker = GetComponent<PlayerAttacker>();

        _directionFlipper = new DirectionFlipper2D(_rigidbody);
    }

    private void OnEnable()
    {
        _inputReader.JumpPressed += OnJump;
        _inputReader.HorizontalMoving += OnMove;
        _inputReader.Attacked += OnAttack;

        _health.Died += OnDie;

        _animationEvents.Attacking += _attacker.DoDamage;
        _animationEvents.Hitted += _attacker.ReloadAttack;
        _animationEvents.Dead += OnDead;
    }

    private void OnDisable()
    {
        _inputReader.JumpPressed -= OnJump;
        _inputReader.HorizontalMoving -= OnMove;
        _inputReader.Attacked -= OnAttack;

        _health.Died -= OnDie;

        _animationEvents.Attacking -= _attacker.DoDamage;
        _animationEvents.Hitted -= _attacker.ReloadAttack;
        _animationEvents.Dead -= OnDead;
    }

    private void Update()
    {
        _animator.SetIsGrounded(_groundChecker.IsGrounded);
        _animator.SetIsFlying(_rigidbody.velocity.y < 0);
    }

    public void Hit()
    {
        _animator.SetHit();
    }

    private void OnJump()
    {
        if (_groundChecker.IsGrounded)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _animator.SetIsJumping(true);
        }
    }

    private void OnMove(float direction)
    {
        if (_rigidbody.isKinematic)
            return;

        bool isMovingPrevState = _isMoving;

        _directionFlipper.FlipHorizontal(direction);

        Vector2 horizontalVelocity = new Vector2(direction, 0) * _movementSpeed;
        _rigidbody.velocity = new Vector2(horizontalVelocity.x, _rigidbody.velocity.y);

        _isMoving = direction != 0;
        _animator.SetIsRunning(_isMoving);
    }

    private void OnAttack()
    {
        _attacker.Attack(_isMoving);
    }

    private void OnDie()
    {
        gameObject.layer = Layers.s_dead;

        _rigidbody.velocity = Vector2.zero;
        _rigidbody.isKinematic = true;

        _animator.SetDie();
    }

    private void OnDead()
    {
        Dead?.Invoke(this);
    }
}
