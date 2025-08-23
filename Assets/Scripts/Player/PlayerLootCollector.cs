using UnityEngine;

public class PlayerLootCollector : MonoBehaviour
{
    [SerializeField] private LootCollector _collector;

    private Wallet _wallet;
    private Health _health;

    private void Awake()
    {
        _wallet = GetComponent<Wallet>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _collector.CoinCollected += OnCoinCollected;
        _collector.BoxCollected += OnBoxCollected;
    }

    private void OnDisable()
    {
        _collector.CoinCollected -= OnCoinCollected;
        _collector.BoxCollected -= OnBoxCollected;
    }

    private void OnCoinCollected(Coin coin)
    {
        _wallet.AddCoins(coin.Value);
    }

    private void OnBoxCollected(Box box)
    {
        _health.Heal(box.HealthValue);
    }
}