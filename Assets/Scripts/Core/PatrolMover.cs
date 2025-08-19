using UnityEngine;

public class PatrolMover
{
    private DirectionFlipper2D _directionFlipper;
    private Rigidbody2D _rigidBody;
    private Route _route;
    private float _movementSpeed;
    private int _currentWaypointIndex = -1;
    private bool _isMoving = true;

    public PatrolMover(Rigidbody2D rigidBody, Route route, float speed)
    {
        _rigidBody = rigidBody;
        _route = route;
        _movementSpeed = speed;

        _rigidBody.position = _route.GetNextWaypointPosition(_currentWaypointIndex);

        _directionFlipper = new DirectionFlipper2D(_rigidBody);
    }

    public void SetRoute(Route route) => _route = route;

    public void SetSpeed(float speed) => _movementSpeed = speed;

    public void Start() => _isMoving = true;

    public void Stop()
    {
        _isMoving = false;
        _rigidBody.velocity = Vector2.zero;
    }

    public bool Update()
    {
        if (!_isMoving)
            return false;

        Vector2 startPosition = _rigidBody.position;
        Vector2 nextPosition = _route.GetNextWaypointPosition(_currentWaypointIndex);

        Vector2 direction = nextPosition - startPosition;

        _directionFlipper.FlipHorizontal(direction);
        _rigidBody.velocity = new Vector2(direction.normalized.x, _rigidBody.velocity.y) * _movementSpeed;

        if (direction.sqrMagnitude <= 0.003f)
        {
            _currentWaypointIndex++;
        }

        return true;
    }
}
