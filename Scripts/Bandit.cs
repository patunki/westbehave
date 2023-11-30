using Godot;
using System;

public partial class Bandit : Entity
{
    [Export]
    public ShotgunScene shotgun;
    public override void _PhysicsProcess(double delta)
    {
        MoveAndSlide();
    }

}
