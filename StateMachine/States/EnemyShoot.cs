using Godot;
using System;
using System.Threading.Tasks;

public partial class EnemyShoot : State
{
    Bandit entity;
    ShotgunScene shotgun;
    Vector2 dir;
    Player player;

    public override void Enter(Entity _entity)
    {
        player = (Player)GetTree().GetFirstNodeInGroup("Player");
        entity = (Bandit)_entity;
        entity.Velocity = new Vector2(0,0);
    }

    public override void Update(double delta)
    {
        entity.shotgun.Shoot(player.GlobalPosition,BulletTarget.Player);
        Vector2 direction = player.GlobalPosition - entity.GlobalPosition;
        float distance = direction.Length();

        if (distance > 200 || player.entityState == EntityState.Dead){
            EmitSignal(SignalName.Transitioned, this,"EnemyWonder");
        }
    }


}
