using Godot;
using System;

public partial class Fishing : State
{
    Entity entity;
    public override void Enter(Entity _entity)
    {
        entity = _entity;
        entity.entityActivity = EntityActivity.Fishing;
        entity.canMove = false;
    }

    public override void Exit()
    {
        entity.entityActivity = EntityActivity.Idle;
        entity.canMove = true;
    }
}
