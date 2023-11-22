using Godot;
using System;

public partial class HealthComponent : Node2D
{
    [Export]
    public int health = 10;
    [Export]
    Label label;
    [Signal]
    public delegate void HealthDepletedEventHandler();

    public void Damage(Attack attack){
        health -= attack.Damage;
        if (health <= 0 ){
            EmitSignal(SignalName.HealthDepleted);
            
        }
    }

}
