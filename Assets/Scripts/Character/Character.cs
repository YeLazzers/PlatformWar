using System;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Character : MonoBehaviour
{
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _movementSpeed;

    private CharacterAnimator _characterAnimator;
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    private DirectionFlipper2D _directionFlipper;

    private void Awake()
    {
        _characterAnimator = GetComponent<CharacterAnimator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _directionFlipper = new DirectionFlipper2D(transform);
    }

    private void OnEnable()
    {
        _inputReader.JumpPressed += OnJump;
        _inputReader.HorizontalMoving += OnMove;
    }

    private void OnDisable()
    {
        _inputReader.JumpPressed -= OnJump;
        _inputReader.HorizontalMoving -= OnMove;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            coin.Pickup();
        }
    }

    private void Update()
    {
        _characterAnimator.SetIsGrounded(_groundChecker.IsGrounded);
        _characterAnimator.SetIsFlying(_rigidBody.velocity.y < 0);
    }

    private void OnJump()
    {
        if (_groundChecker.IsGrounded)
        {
            _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _characterAnimator.SetIsJumping(true);
        }
    }

    private void OnMove(float direction)
    {
        _directionFlipper.FlipHorizontal(direction);

        Vector2 velocity = new Vector2(direction, 0) * _movementSpeed * Time.fixedDeltaTime;
        _rigidBody.velocity = new Vector2(velocity.x, _rigidBody.velocity.y);

        _characterAnimator.SetIsRunning(direction != 0);
    }
}
