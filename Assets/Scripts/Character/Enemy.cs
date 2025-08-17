using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Route route;
    [SerializeField] private float _movementSpeed;

    private PatrolMover _patrolMover;
    private CharacterAnimator _characterAnimator;

    private void Awake()
    {
        _patrolMover = new PatrolMover(transform, route, _movementSpeed);

        _characterAnimator = GetComponent<CharacterAnimator>();
    }

    private void Update()
    {
        bool isMoved = _patrolMover.Update(Time.deltaTime);
        _characterAnimator.SetIsRunning(isMoved);

    }
}
