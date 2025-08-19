using System;
using UnityEngine;

public class FollowMover: MoverBase
{
    private Transform _target;
    private float _reachDistance = 0.003f;

    public event Action<Transform> TargetReached;

    public FollowMover(Rigidbody2D rigidbody, float speed, float distance): base(rigidbody, speed)
    {
        _reachDistance = distance;
    }


    public void SetTarget(Transform target) => _target = target;

    public override void Move()
    {
        Vector3 direction = _target.transform.position - _rigidbody.transform.position;
        if (direction.sqrMagnitude >= _reachDistance)
            base.Move(direction);
        else
        {
            TargetReached?.Invoke(_target);
        }
    }
}
