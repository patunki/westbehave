using Godot;
using System;

public partial class EnemyFollow : State
{
    Entity player;
    Entity enemy;
    [Export]
    int moveSpeed;
    public override void Enter(Entity entity)
    {
        enemy = entity;
        player = (Entity)GetTree().GetFirstNodeInGroup("Player");
    }

    public override void PhysicsUpdate(double delta)
    {
        Vector2 direction = player.GlobalPosition - enemy.GlobalPosition;
        float distance = direction.Length();

        if (distance > 120 || player.entityState == EntityState.Dead){
            EmitSignal(SignalName.Transitioned,this,"EnemyIdle");
        }

        if (distance < 100 && distance > 20){
            enemy.Velocity = direction.Normalized() * moveSpeed;
        }

        if (distance < 20){
            EmitSignal(SignalName.Transitioned,this,"EnemyPunch");
        }

    }
}
