using System.Collections;
using UnityEngine;

public class PatrolMover : MoverBase
{
    private float nextWaypointWaitTime = 1f;
    private Route _route;
    private Waypoint _target;

    public PatrolMover(Rigidbody2D rigidbody, float speed, ICoroutineRunner coroutineRunner, Route route) : base(rigidbody, speed, coroutineRunner)
    {
        SetRoute(route);
    }

    private IEnumerator WaitForNextWaypoint()
    {
        Deactivate();
        yield return new WaitForSeconds(nextWaypointWaitTime);
        _target = _route.GetNextWaypoint(_target);
        Activate();
    }

    public void SetRoute(Route route)
    {
        _route = route;
    }

    public new void Activate()
    {
        base.Activate();

        if (_target == null)
            GoToNearestWaypoint();
    }

    public void GoToNearestWaypoint()
    {
        _target = _route.GetNearestWaypoint(Rigidbody.transform.position);
    }

    public override void Move()
    {
        if (!IsActive)
            return;

        Vector3 direction = _target.transform.position - Rigidbody.transform.position;
        if (direction.sqrMagnitude >= 0.003f)
            Move(direction);
        else
        {
            CoroutineRunner.StartCoroutine(WaitForNextWaypoint());
        }
    }
}
