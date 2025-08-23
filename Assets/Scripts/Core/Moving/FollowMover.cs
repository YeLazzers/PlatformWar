using System;
using UnityEngine;

public class FollowMover : MoverBase
{
    private Transform _target;
    private float _reachDistance;

    public FollowMover(Rigidbody2D rigidbody, float speed, ICoroutineRunner coroutineRunner, float distance) : base(rigidbody, speed, coroutineRunner)
    {
        _reachDistance = distance;
    }

    public event Action<Transform> TargetReached;

    public override void Move()
    {
        if (Rigidbody.transform.position.IsEnoughClose(_target.transform.position, _reachDistance) == false)
            base.Move(_target.transform.position - Rigidbody.transform.position);
        else
            TargetReached?.Invoke(_target);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
