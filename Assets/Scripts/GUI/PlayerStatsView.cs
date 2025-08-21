using TMPro;
using UnityEngine;

public class PlayerStatsView : MonoBehaviour
{
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private TextMeshProUGUI _healthTMP;
    [SerializeField] private TextMeshProUGUI _coinsTMP;

    private void OnEnable()
    {
        _playerSpawner.Spawned += OnPlayerSpawned;
        _playerSpawner.Destroyed += OnPlayerDestroyed;
    }

    private void OnDisable()
    {
        _playerSpawner.Spawned -= OnPlayerSpawned;
        _playerSpawner.Destroyed -= OnPlayerDestroyed;
    }

    private void OnPlayerSpawned(Player player)
    {
        player.Health.Changed += UpdateHealthUI;
        UpdateHealthUI(player.Health.Current, player.Health.Max);

        player.Wallet.CoinsChanged += UpdateCoinsUI;
        UpdateCoinsUI(player.Wallet.Coins);
    }

    private void OnPlayerDestroyed(Player player)
    {
        player.Health.Changed -= UpdateHealthUI;
        player.Wallet.CoinsChanged -= UpdateCoinsUI;
    }

    private void UpdateHealthUI(int current, int max)
    {
        _healthTMP.text = $"{current} / {max}";
    }

    private void UpdateCoinsUI(int amount)
    {
        _coinsTMP.text = amount.ToString();
    }
}