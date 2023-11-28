using Godot;
using System;

public partial class EnemyPunch : State
{
    Entity player;
    [Export]
    AnimationPlayer animationPlayer;
    [Export]
    Entity enemy;
    public override void Enter()
    {
        player = (Entity)GetTree().GetFirstNodeInGroup("Player");
        enemy.Velocity = new Vector2(0,0);
        animationPlayer.Play("Punch");
        Timer timer = new Timer();
        AddChild(timer);
        timer.Start(1);
        timer.Timeout += Follow;
        timer.Timeout += timer.QueueFree;
        
    }

    void Follow(){
        EmitSignal("Transitioned",this,"EnemyFollow");
    }

}
