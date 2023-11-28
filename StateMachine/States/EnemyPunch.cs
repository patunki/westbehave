using Godot;
using System;

public partial class EnemyPunch : State
{
    Entity player;
    [Export]
    AnimationPlayer animationPlayer;
    Entity enemy;
    public override void Enter(Entity entity)
    {
        enemy = entity;
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
        EmitSignal(SignalName.Transitioned,this,"EnemyFollow");
    }

}
