using Godot;
using System;

public enum AttackType{
    bullet,
    axe
}

public partial class Attack : Node
{
    public int Damage;
    public int KnockBack;
    public AttackType attackType;
}
