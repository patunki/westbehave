using Godot;
using System;

public partial class HurtBoxComponent : Area2D
{
    [Export]
    public HealthComponent healthComponent;

    void _on_body_entered(Node2D body){
        Attack attack = new Attack();
        attack.Damage = 10;
        healthComponent.Damage(attack);
    }
}
