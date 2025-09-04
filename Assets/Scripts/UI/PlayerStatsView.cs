using TMPro;
using Unity.VisualScripting;
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

    private void UpdateHealthUI(float current, float max)
    {
        _healthTMP.text = $"{current.ConvertTo<int>()} / {max.ConvertTo<int>()}";
    }

    private void UpdateCoinsUI(int amount)
    {
        _coinsTMP.text = amount.ToString();
    }
}