using Godot;
using System;

public partial class tool_axe : Tool
{
    public Attack Attack(){

        Attack attack = new Attack();
        attack.Damage = 5;
        attack.attackType = AttackType.axe;
        return attack;
    }
}
