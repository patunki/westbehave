using Godot;
using System;

public partial class Idle : State
{

    public override void Enter(Entity entity)
    {
        entity.canMove = true;
    }

    public override void PhysicsUpdate(double delta)
    {
        
    }
}
