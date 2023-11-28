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

        if (distance < 100){
            enemy.Velocity = direction.Normalized() * moveSpeed;
        }
        else {
            enemy.Velocity = new Vector2(0,0);
        }

        if (distance > 100){
            EmitSignal("Transitioned",this,"EnemyIdle");
        }
    }
}
