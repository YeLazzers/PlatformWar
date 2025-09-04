using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] private float _max;

    private float _current;

    public float Current => _current;
    public float Max => _max;
    public event Action<float, float> Changed;
    public event Action Died;

    private void Awake()
    {
        _current = _max;
    }

    public float TakeHeal(float amount)
    {
        float startHp = _current;

        if (amount > 0)
        {

            _current = Mathf.Min(_current + amount, _max);
            Changed?.Invoke(_current, _max);
        }

        return _current - startHp;
    }

    public float TakeDamage(float amount)
    {
        float startHp = _current;

        if (amount > 0)
        {

            _current = Mathf.Max(0, _current - amount);
            Changed?.Invoke(_current, _max);

            if (Mathf.Approximately(_current, 0))
                Died?.Invoke();

        }

        return startHp - _current;
    }

    public void Reset()
    {
        _current = _max;
        Changed?.Invoke(_current, _max);
    }
}