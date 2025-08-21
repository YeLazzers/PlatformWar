using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour, ICoroutineRunner, IPushable, IHitable
{
    [SerializeField] private PlayerDetector _playerDetector;
    [SerializeField] private Route route;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _hitForce;
    [SerializeField] private float _distanceToAttack;

    private Rigidbody2D _rigidBody;
    private CharacterAnimator _characterAnimator;
    private EnemyAttacker _attacker;
    private PatrolMover _patrolMover;
    private FollowMover _followMover;
    private MoverBase _currentMover;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _characterAnimator = GetComponent<CharacterAnimator>();
        _attacker = GetComponent<EnemyAttacker>();

        _patrolMover = new PatrolMover(_rigidBody, _movementSpeed, this, route);
        _followMover = new FollowMover(_rigidBody, _movementSpeed, this, _distanceToAttack);

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
        StopMoving();

        _attacker.StartAttacking(target);
    }
    private void OnPlayerMissed(Player player)
    {
        _attacker.StopAttacking();

        SwitchToPatrolMover();
    }

    private void OnPlayerDetected(Player player)
    {
        _followMover.SetTarget(player.transform);
        SwitchToFollowMover();
    }

    public void ContinueMoving()
    {
        _currentMover.Activate();
    }

    public void StopMoving()
    {
        _currentMover.Deactivate();
    }

    public void Push(Vector3 direction)
    {
        _rigidBody.AddForce(direction.normalized * _hitForce, ForceMode2D.Impulse);
    }

    public void Hit()
    {
        StopMoving();
        _characterAnimator.SetHit();
        _attacker.ReloadAttack();
    }
}
