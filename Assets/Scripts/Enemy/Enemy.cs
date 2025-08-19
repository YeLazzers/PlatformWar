using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(CharacterHealth))]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerDetector _playerDetector;
    [SerializeField] private Route route;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _hitForce;
    [SerializeField] private float _distanceToAttack;

    private Rigidbody2D _rigidBody;
    private CharacterAnimator _characterAnimator;
    private CharacterHealth _health;
    private EnemyAttacker _attacker;
    private PatrolMover _patrolMover;
    private FollowMover _followMover;
    private MoverBase _currentMover;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _characterAnimator = GetComponent<CharacterAnimator>();
        _health = GetComponent<CharacterHealth>();
        _attacker = GetComponent<EnemyAttacker>();

        _patrolMover = new PatrolMover(_rigidBody, _movementSpeed, route);
        _followMover = new FollowMover(_rigidBody, _movementSpeed, _distanceToAttack);
        
        _currentMover = _patrolMover;

    }

    private void OnEnable()
    {
        _followMover.TargetReached += OnTargetReached;
        _playerDetector.Detected += OnPlayerDetected;
        _playerDetector.Missed += OnPlayerMissed;
    }

    private void OnDisable()
    {
        _followMover.TargetReached -= OnTargetReached;
        _playerDetector.Detected -= OnPlayerDetected;
        _playerDetector.Missed -= OnPlayerMissed;
    }

    private void Start()
    {
        _patrolMover.Activate();
    }

    private void Update()
    {
        _currentMover.Move();
        _characterAnimator.SetIsRunning(_currentMover.IsActive);
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
        StartCoroutine(_attacker.Attacking(target, this));
    }
    private void OnPlayerMissed(Player player)
    {
        SwitchToPatrolMover();
    }

    private void OnPlayerDetected(Player player)
    {
        _followMover.SetTarget(player.transform);
        SwitchToFollowMover();
    }

    public void ContinueMoving() => _currentMover.Activate();

    public void StopMoving() => _currentMover.Deactivate();

    public void TakeDamage(int amount, Transform attacker)
    {
        StopMoving();

        _characterAnimator.SetHit();
        _rigidBody.AddForce((transform.position - attacker.position).normalized * _hitForce, ForceMode2D.Impulse);

        _health.ApplyDamage(amount);

        _attacker.ReloadAttack();
    }
}
