using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class LootCollector : MonoBehaviour
{
    public event Action<Coin> CoinCollected;
    public event Action<Box> BoxCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out LootBase loot))
        {
            loot.Interact();
        }

        if (collision.TryGetComponent(out Coin coin))
        {
            CoinCollected?.Invoke(coin);
        }

        if (collision.TryGetComponent(out Box box))
        {
            BoxCollected?.Invoke(box);
        }
    }
}
