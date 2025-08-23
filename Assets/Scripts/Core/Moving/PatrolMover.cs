using System.Collections;
using UnityEngine;

public class PatrolMover : MoverBase
{
    private float _reachDistance = 0.03f;
    private float nextWaypointWaitTime = 1f;
    private Route _route;
    private Waypoint _target;

    public PatrolMover(Rigidbody2D rigidbody, float speed, ICoroutineRunner coroutineRunner, Route route) : base(rigidbody, speed, coroutineRunner)
    {
        SetRoute(route);
    }

    public override void Move()
    {
        if (IsActive == false)
            return;

        if (Rigidbody.transform.position.IsEnoughClose(_target.transform.position, _reachDistance) == false)
            base.Move(_target.transform.position - Rigidbody.transform.position);
        else
            CoroutineRunner.StartCoroutine(WaitForNextWaypoint());
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

    private IEnumerator WaitForNextWaypoint()
    {
        Deactivate();
        yield return new WaitForSeconds(nextWaypointWaitTime);
        _target = _route.GetNextWaypoint(_target);
        Activate();
    }
}
