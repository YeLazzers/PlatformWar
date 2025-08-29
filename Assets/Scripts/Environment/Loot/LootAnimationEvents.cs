using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LootAnimationEvents : MonoBehaviour
{
    public event Action<LootAnimationEvents> Interacted;

    public void InvokeInteractedEvent() => Interacted?.Invoke(this);
}