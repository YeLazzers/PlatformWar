using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Wallet))]
[RequireComponent(typeof(PlayerAttacker))]
[RequireComponent(typeof(PlayerFx))]
public class Player : MonoBehaviour, IHitable
{
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _movementSpeed;

    private Rigidbody2D _rigidbody;
    private CharacterAnimator _characterAnimator;
    private Health _health;
    private Wallet _wallet;
    private PlayerAttacker _attacker;
    private PlayerFx _playerFx;
    private DirectionFlipper2D _directionFlipper;
    private bool _isMoving;

    public Health Health => _health;
    public Wallet Wallet => _wallet;
    public event Action<Player> Destroyed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _characterAnimator = GetComponent<CharacterAnimator>();
        _health = GetComponent<Health>();
        _wallet = GetComponent<Wallet>();
        _attacker = GetComponent<PlayerAttacker>();
        _playerFx = GetComponent<PlayerFx>();

        _directionFlipper = new DirectionFlipper2D(_rigidbody);
    }

    private void OnEnable()
    {
        _inputReader.JumpPressed += OnJump;
        _inputReader.HorizontalMoving += OnMove;
        _inputReader.Attacked += OnAttack;

        _health.Died += OnDie;
    }

    private void OnDisable()
    {
        _inputReader.JumpPressed -= OnJump;
        _inputReader.HorizontalMoving -= OnMove;
        _inputReader.Attacked -= OnAttack;

        _health.Died -= OnDie;
    }

    private void Update()
    {
        _characterAnimator.SetIsGrounded(_groundChecker.IsGrounded);
        _characterAnimator.SetIsFlying(_rigidbody.velocity.y < 0);
    }

    public void Heal(int amount)
    {
        _health.Heal(amount);
    }

    private void OnJump()
    {
        if (_groundChecker.IsGrounded)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _characterAnimator.SetIsJumping(true);
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
        _characterAnimator.SetIsRunning(_isMoving);

        if (isMovingPrevState == false && _isMoving && _groundChecker.IsGrounded)
        {
            _playerFx.GroundDust();
        }
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

        _characterAnimator.SetDie();
    }

    public void OnDieAnimationEnd()
    {
        Destroyed?.Invoke(this);
    }

    public void Hit()
    {
        _characterAnimator.SetHit();

        _attacker.ReloadAttack();
    }
}
