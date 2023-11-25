using Godot;
using System;

public partial class HurtBoxComponent : Area2D
{
    [Export]
    public HealthComponent healthComponent;

    public void Damage(Attack attack){
        if (GetParent() is not Tree){
            
            healthComponent.Damage(attack);
        
        
        
        }
    }
    public void Hit(Attack attack){
        if (GetParent() is Tree && attack.attackType == AttackType.axe){
            healthComponent.Damage(attack);
        }
    }

}
