using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class LootBase : MonoBehaviour
{
    protected Animator AnimatorComp;

    public bool IsInteracted { get; protected set; }

    private void Awake()
    {
        AnimatorComp = GetComponent<Animator>();
    }

    protected void Interact()
    {
        IsInteracted = true;
    }

    protected abstract void OnTriggerEnter2D(Collider2D other);
}