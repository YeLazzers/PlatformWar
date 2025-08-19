using System.Collections;
using UnityEngine;

public class Coin : LootBase
{
    private static readonly int _pickupHash = Animator.StringToHash("Picked");

    [SerializeField] private int _coinsValue;

    private WaitForSeconds _destroyTimer;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isInteracted && other.TryGetComponent(out Player player))
        {
            player.CharacterWallet.AddCoins(_coinsValue);
            Interact();
        }
    }
    private new void Interact()
    {
        base.Interact();
        _animator.SetTrigger(_pickupHash);

        StartCoroutine(DelayedDestroy());
    }

    private IEnumerator DelayedDestroy()
    {
        yield return _destroyTimer;
        Destroy(gameObject);
    }
}
