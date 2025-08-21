using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] private int _maxHealth;

    private int _health;

    public int Current => _health;
    public int Max => _maxHealth;
    public event Action<int, int> Changed;
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
            Changed?.Invoke(_health, _maxHealth);
        }
    }

    public void TakeDamage(int amount)
    {
        if (amount > 0)
        {
            _health = Mathf.Max(0, _health - amount);
            Changed?.Invoke(_health, _maxHealth);

            if (_health == 0)
                Died?.Invoke();
        }
    }
}