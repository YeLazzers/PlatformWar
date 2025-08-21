using System.Collections;
using UnityEngine;

public class Coin : LootBase
{
    private static readonly int s_pickupHash = Animator.StringToHash("Picked");

    [SerializeField] private int _coinsValue;

    private WaitForSeconds _destroyTimer;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsInteracted && other.TryGetComponent(out Wallet wallet))
        {
            wallet.AddCoins(_coinsValue);
            Interact();
        }
    }
    private new void Interact()
    {
        base.Interact();
        AnimatorComp.SetTrigger(s_pickupHash);

        StartCoroutine(DelayedDestroy());
    }

    private IEnumerator DelayedDestroy()
    {
        yield return _destroyTimer;
        Destroy(gameObject);
    }
}
