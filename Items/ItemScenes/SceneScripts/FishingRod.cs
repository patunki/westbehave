using Godot;
using System;
using System.Drawing;

public partial class FishingRod : Node2D
{
    Sprite2D koho;
    Sprite2D rod;
    Godot.Color lineColor = new Godot.Color(1, 0, 0, 1);
    AnimationPlayer animationPlayer;

    public override void _Ready()
    {
        koho = GetNode<Sprite2D>("Koho");
        rod = GetNode<Sprite2D>("Rod");
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public void MyItem(Item item){

    }

    public override void _PhysicsProcess(double delta)
    {
        QueueRedraw();
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("attack")){
            animationPlayer.Play("Throw");
        }
    }

    public override void _Draw()
    {
        DrawLine(koho.Position, rod.Position,lineColor,1);
    }

}
