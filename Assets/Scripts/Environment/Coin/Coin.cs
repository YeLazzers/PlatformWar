using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Animator))]
public class Coin : MonoBehaviour
{
    private static readonly int _pickupHash = Animator.StringToHash("Picked");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator> ();
    }
    public void Pickup()
    {
        _animator.SetTrigger(_pickupHash);
        StartCoroutine(DelayedDestroy());
    }

    private IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
