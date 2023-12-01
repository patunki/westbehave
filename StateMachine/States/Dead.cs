using Godot;
using System;

public partial class Dead : State
{

    public override void Enter(Entity entity)
    {
        entity.Velocity = new Vector2(0,0);
        entity.Modulate = new Color (1,0,0);
        entity.canMove = false;
    }
    
}
