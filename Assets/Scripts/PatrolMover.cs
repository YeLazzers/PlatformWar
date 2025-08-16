using System.Collections;
using UnityEngine;

public class PatrolMover
{
    private Transform _characterTransform;
    private Route _route;
    private float _movementSpeed;
    private int _currentWaypointIndex = -1;

    public PatrolMover(Transform characterTransform, Route route, float speed)
    {
        _characterTransform = characterTransform;
        _route = route;
        _movementSpeed = speed;

        _characterTransform.position = _route.GetNextWaypointPosition(_currentWaypointIndex);
    }

    public void SetRoute(Route route) => _route = route;
    public void SetSpeed(float speed) => _movementSpeed = speed;

    public bool Update(float deltaTime)
    {
        Vector3 startPosition = _characterTransform.position;
        Vector3 nextPosition = _route.GetNextWaypointPosition(_currentWaypointIndex);

        _characterTransform.transform.right = nextPosition - _characterTransform.position;
        _characterTransform.position = Vector3.MoveTowards(_characterTransform.position, nextPosition, _movementSpeed * Time.deltaTime);

        if (_characterTransform.position == nextPosition)
            _currentWaypointIndex++;

        return (_characterTransform.position - startPosition).magnitude > 0;
    }
}
