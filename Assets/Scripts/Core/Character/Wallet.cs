using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _coins = 0;

    public int Coins => _coins;
    public event Action<int> CoinsChanged;

    public void AddCoins(int amount)
    {
        _coins += amount;

        CoinsChanged?.Invoke(_coins);
    }

    public void AddCoin()
    {
        _coins++;

        CoinsChanged?.Invoke(_coins);
    }
}