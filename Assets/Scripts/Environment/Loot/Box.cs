using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Box : LootBase
{
    private static readonly int _interactHash = Animator.StringToHash("Interact");

    [SerializeField] private int _healValue;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isInteracted && other.TryGetComponent(out Player player))
        {
            player.CharacterHealth.Heal(_healValue);
            Interact();
        }
    }

    private new void Interact()
    {
        base.Interact();
        _animator.SetTrigger(_interactHash);
    }
}
