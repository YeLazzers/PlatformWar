using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(CharacterAnimationEvents))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour, ICoroutineRunner, IKnockbackable, IHitable
{
    [SerializeField] private PlayerDetector _playerDetector;
    [SerializeField] private Route route;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _knockbackForce;
    [SerializeField] private float _distanceToAttack;

    private Rigidbody2D _rigidbody;
    private CharacterAnimator _animator;
    private CharacterAnimationEvents _animationEvents;
    private Health _health;
    private EnemyAttacker _attacker;
    private PatrolMover _patrolMover;
    private FollowMover _followMover;
    private MoverBase _currentMover;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<CharacterAnimator>();
        _animationEvents = GetComponent<CharacterAnimationEvents>();
        _attacker = GetComponent<EnemyAttacker>();
        _health = GetComponent<Health>();

        _patrolMover = new PatrolMover(_rigidbody, _movementSpeed, this, route);
        _followMover = new FollowMover(_rigidbody, _movementSpeed, this, _distanceToAttack);

        _currentMover = _patrolMover;

    }

    private void OnEnable()
    {
        _followMover.TargetReached += OnTargetReached;

        if (_playerDetector != null)
        {
            _playerDetector.Detected += OnPlayerDetected;
            _playerDetector.Missed += OnPlayerMissed;
        }

        _animationEvents.Hitted += OnHitted;

        if (_attacker != null)
        {
            _animationEvents.Attacking += _attacker.DoDamage;
            _attacker.TargetEscaped += ContinueMoving;
        }

        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _followMover.TargetReached -= OnTargetReached;

        if (_playerDetector != null)
        {
            _playerDetector.Detected -= OnPlayerDetected;
            _playerDetector.Missed -= OnPlayerMissed;
        }

        _animationEvents.Hitted -= OnHitted;

        if (_attacker != null)
        {
            _animationEvents.Attacking -= _attacker.DoDamage;
            _attacker.TargetEscaped += ContinueMoving;
        }

        _health.Died -= OnDied;
    }

    private void Start()
    {
        _patrolMover.Activate();
    }

    private void Update()
    {
        _currentMover.Move();
        _animator.SetIsRunning(_currentMover.IsActive);
    }

    public void ContinueMoving() =>
        _currentMover.Activate();

    public void StopMoving()
    {
        _currentMover.Deactivate();
    }

    public void Knockback(Vector3 direction) =>
        _rigidbody.AddForce(direction.normalized * _knockbackForce, ForceMode2D.Impulse);

    public void Hit()
    {
        StopMoving();

        _animator.SetHit();
    }

    private void SwitchToFollowMover()
    {
        _patrolMover.Deactivate();
        _currentMover = _followMover;
        _followMover.Activate();
    }

    private void SwitchToPatrolMover()
    {
        _followMover.Deactivate();
        _currentMover = _patrolMover;
        _patrolMover.Activate();
        _patrolMover.GoToNearestWaypoint();
    }

    private void OnTargetReached(Transform target)
    {
        StopMoving();

        _attacker?.StartAttacking(target);
    }
    private void OnPlayerMissed(Player player)
    {
        _attacker?.StopAttacking();

        SwitchToPatrolMover();
    }

    private void OnPlayerDetected(Player player)
    {
        _followMover.SetTarget(player.transform);
        SwitchToFollowMover();
    }

    private void OnHitted()
    {
        ContinueMoving();

        _attacker?.ReloadAttack();
    }

    private void OnDied()
    {
        Destroy(gameObject);
    }
}
