using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class LootBase : MonoBehaviour
{
    protected Animator _animator;
    protected bool _isInteracted = false;

    public bool IsInteracted => _isInteracted;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    protected void Interact()
    {
        _isInteracted = true;
    }

    protected abstract void OnTriggerEnter2D(Collider2D other);
}