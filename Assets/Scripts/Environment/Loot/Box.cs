using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Box : LootBase
{
    private static readonly int s_interactHash = Animator.StringToHash("Interact");

    [SerializeField] private int _healValue;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsInteracted && other.TryGetComponent(out IHealable healable))
        {
            healable.Heal(_healValue);
            Interact();
        }
    }

    private new void Interact()
    {
        base.Interact();
        AnimatorComp.SetTrigger(s_interactHash);
    }
}
