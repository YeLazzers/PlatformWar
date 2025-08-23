using UnityEngine;

public class Coin : LootBase
{
    [SerializeField] private int _value;

    public int Value => _value;
}
