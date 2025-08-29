using TMPro;
using UnityEngine;

public class PlayerStatsView : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TextMeshProUGUI _healthTMP;
    [SerializeField] private TextMeshProUGUI _coinsTMP;

    private void OnEnable()
    {
        _player.Health.Changed += UpdateHealthUI;
        UpdateHealthUI(_player.Health.Current, _player.Health.Max);

        _player.Wallet.CoinsChanged += UpdateCoinsUI;
        UpdateCoinsUI(_player.Wallet.Coins);
    }

    private void OnDisable()
    {
        _player.Health.Changed -= UpdateHealthUI;
        _player.Wallet.CoinsChanged -= UpdateCoinsUI;
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