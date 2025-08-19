using UnityEngine;

public static class Layers
{
    public static readonly int Player = LayerMask.NameToLayer("Player");
    public static readonly int Enemy = LayerMask.NameToLayer("Enemy");
    public static readonly int Ground = LayerMask.NameToLayer("Ground");
    public static readonly int Dead = LayerMask.NameToLayer("Dead");
}