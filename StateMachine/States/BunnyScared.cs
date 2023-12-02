using Godot;
using System;

public partial class BunnyScared : State
{

    float time;
    Entity entity;
    Player player;
    [Export]
    int speed = 50;
    public override void Enter(Entity _entity)
    {
        entity = _entity;
        player = GetTree().GetFirstNodeInGroup("Player") as Player;
        Vector2 dir = entity.GlobalPosition - player.GlobalPosition;
        entity.Velocity = dir * speed;
        time = 3;
    }

    public override void PhysicsUpdate(double delta)
    {

        time -= (float)delta;
        if (time <= 0){
            EmitSignal(SignalName.Transitioned,this,"Idle");
        }
    
    }


}
