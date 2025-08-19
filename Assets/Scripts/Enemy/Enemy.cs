using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(CharacterHealth))]
public class Enemy : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] private Route route;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _hitForce;

    private PatrolMover _patrolMover;
    private CharacterAnimator _characterAnimator;
    private CharacterHealth _health;
    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _characterAnimator = GetComponent<CharacterAnimator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _health = GetComponent<CharacterHealth>();

        _patrolMover = new PatrolMover(_rigidBody, route, _movementSpeed);
    }

    private void Update()
    {
        bool isMoved = _patrolMover.Update();
        _characterAnimator.SetIsRunning(isMoved);
    }

    public void PatrolContinue() => _patrolMover.Start();

    public void PatrolStop() => _patrolMover.Stop();

    public void TakeDamage(int amount, Transform attacker)
    {
        PatrolStop();

        _characterAnimator.SetHit();
        _rigidBody.AddForce((transform.position - attacker.position).normalized * _hitForce, ForceMode2D.Impulse);

        _health.ApplyDamage(amount);
    }

    public void Heal(int amount)
    {
        throw new System.NotImplementedException();
    }
}
