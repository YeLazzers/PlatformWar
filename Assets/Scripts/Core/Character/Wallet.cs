using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _coins = 0;

    public event Action<int> CoinsChanged;

    public int Coins => _coins;

    public void AddCoins(int amount)
    {
        if (amount > 0)
        {
            _coins += amount;

            CoinsChanged?.Invoke(_coins);
        }
    }

    public void AddCoin()
    {
        _coins++;

        CoinsChanged?.Invoke(_coins);
    }
}