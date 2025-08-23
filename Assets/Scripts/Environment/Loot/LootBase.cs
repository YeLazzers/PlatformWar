using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class LootBase : MonoBehaviour
{
    private static readonly int s_interactHash = Animator.StringToHash("Interact");

    protected Animator AnimatorComponent;

    public LootAnimationEvents AnimationEvents { get; protected set; }
    public bool IsInteracted { get; protected set; }

    private void Awake()
    {
        AnimatorComponent = GetComponent<Animator>();

        AnimationEvents = GetComponent<LootAnimationEvents>();
    }

    public void Interact()
    {
        IsInteracted = true;
        AnimatorComponent.SetTrigger(s_interactHash);
    }
}