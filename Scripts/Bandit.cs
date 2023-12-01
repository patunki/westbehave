using Godot;
using System;

public partial class Bandit : Entity
{
    [Export]
    public ShotgunScene shotgun;
    [Export]
    Item item;

    public override void _Ready()
    {
        shotgun.MyItem(item,this);
    }
    public override void _PhysicsProcess(double delta)
    {
        MoveAndSlide();
    }

}
