using Godot;
using System;

public partial class ToolAxe : Tool
{
    public Attack Attack(){

        Attack attack = new Attack();
        attack.Damage = 5;
        attack.attackType = AttackType.axe;
        return attack;
    }
}
