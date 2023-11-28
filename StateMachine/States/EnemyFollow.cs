using Godot;
using System;

public partial class EnemyFollow : State
{
    Entity player;
    [Export]
    Entity enemy;
    [Export]
    int moveSpeed;
    public override void Enter()
    {
        GD.Print("Entered EnemyFollow");
        player = (Entity)GetTree().GetFirstNodeInGroup("Player");
    }

    public override void PhysicsUpdate(double delta)
    {
        Vector2 direction = player.GlobalPosition - enemy.GlobalPosition;
        float distance = direction.Length();
        if (distance < 100 && distance > 20){
            enemy.Velocity = direction.Normalized() * moveSpeed;
        }

        if (distance < 20){
            EmitSignal("Transitioned",this,"EnemyPunch");
        }

        if (distance > 120){
            EmitSignal("Transitioned",this,"EnemyIdle");
        }
    }
}
