using Godot;
using System;

public partial class Bunny : Entity
{
    public override void _PhysicsProcess(double delta)
    {
        MoveAndSlide();
    }
}
