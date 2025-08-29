using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] private int _max;

    private int _current;

    public int Current => _current;
    public int Max => _max;
    public event Action<int, int> Changed;
    public event Action Died;

    private void Awake()
    {
        _current = _max;
    }

    public void TakeHeal(int amount)
    {
        if (amount > 0)
        {
            _current = Mathf.Min(_current + amount, _max);
            Changed?.Invoke(_current, _max);
        }
    }

    public void TakeDamage(int amount)
    {
        if (amount > 0)
        {
            _current = Mathf.Max(0, _current - amount);
            Changed?.Invoke(_current, _max);

            if (_current == 0)
                Died?.Invoke();
        }
    }

    public void Reset()
    {
        _current = _max;
        Changed?.Invoke(_current, _max);
    }
}