using Godot;
using System;
using System.Drawing;

public partial class FishingRod : Node2D
{
    RigidBody2D koho;
    StaticBody2D rod;
    Godot.Color lineColor = new Godot.Color(1, 0, 0, 1);

    public override void _Ready()
    {
        koho = GetNode<RigidBody2D>("KohoBody");
        rod = GetNode<StaticBody2D>("RodBody");
    }

    public override void _PhysicsProcess(double delta)
    {
        QueueRedraw();
    }

    public override void _Draw()
    {
        DrawLine(koho.Position, rod.Position,lineColor,1);
    }

}
