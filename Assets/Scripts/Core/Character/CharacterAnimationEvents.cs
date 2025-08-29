using System;
using UnityEngine;

public class CharacterAnimationEvents : MonoBehaviour
{
    public event Action Attacking;
    public event Action Attacked;

    public event Action Dead;

    public event Action Hitted;

    public void InvokeAttackingEvent() => Attacking?.Invoke();
    public void InvokeAttackedEvent() => Attacked?.Invoke();

    public void InvokeDeadEvent() => Dead?.Invoke();

    public void InvokeHittedEvent() => Hitted?.Invoke();
}