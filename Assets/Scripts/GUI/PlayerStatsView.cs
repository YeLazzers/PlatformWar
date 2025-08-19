using TMPro;
using UnityEngine;

public class PlayerStatsView: MonoBehaviour
{
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private TextMeshProUGUI _healthTMP;
    [SerializeField] private TextMeshProUGUI _coinsTMP;

    private void OnEnable()
    {
        _playerSpawner.PlayerSpawned += OnPlayerSpawned;
    }

    private void OnDisable()
    {
        _playerSpawner.PlayerSpawned -= OnPlayerSpawned;
    }

    private void OnPlayerSpawned(Player player)
    {
        player.CharacterHealth.HealthChanged += UpdateHealthUI;
        UpdateHealthUI(player.CharacterHealth.Health, player.CharacterHealth.MaxHealth);

        player.CharacterWallet.CoinsChanged += UpdateCoinsUI;
        UpdateCoinsUI(player.CharacterWallet.Coins);
    }

    private void UpdateHealthUI(int current, int max) => _healthTMP.text = $"{current} / {max}";

    private void UpdateCoinsUI(int amount)
    {
        _coinsTMP.text = amount.ToString();
    }
}