using UnityEngine;

public class Box : LootBase
{
    [SerializeField] private int _value;
    
    public int HealthValue => _value;
}
