using Godot;
using System;

public partial class HurtBoxComponent : Area2D
{
    [Export]
    public HealthComponent healthComponent;
}
