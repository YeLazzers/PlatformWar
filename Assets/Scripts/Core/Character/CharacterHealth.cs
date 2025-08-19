using System;
using UnityEngine;

public class CharacterHealth: MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private int _health;

    public int Health => _health;
    public int MaxHealth => _maxHealth;
    public event Action<int, int> HealthChanged;
    public event Action Died;

    private void Awake()
    {
        _health = _maxHealth;
    }

    public void Heal(int amount)
    {
        if (amount > 0)
        {
            _health = Mathf.Min(_health + amount, _maxHealth);
            HealthChanged?.Invoke(_health, _maxHealth);
        }
    }

    public void ApplyDamage(int amount)
    {
        if (amount > 0)
        {
            _health = Mathf.Max(0, _health - amount);
            HealthChanged?.Invoke(_health, _maxHealth);

            if (_health == 0)
                Died?.Invoke();
        }
    }
}