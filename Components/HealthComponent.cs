using Godot;
using System;

public partial class HealthComponent : Node2D
{
    [Export]
    public int health = 10;
    [Export]
    Label label;

    public void Damage(Attack attack){


    }

}
