using UnityEngine;

public static class Layers
{
    public static readonly int s_player = LayerMask.NameToLayer("Player");
    public static readonly int s_enemy = LayerMask.NameToLayer("Enemy");
    public static readonly int s_ground = LayerMask.NameToLayer("Ground");
    public static readonly int s_dead = LayerMask.NameToLayer("Dead");
}