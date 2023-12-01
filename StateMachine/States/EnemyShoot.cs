using Godot;
using System;
using System.Threading.Tasks;

public partial class EnemyShoot : State
{
    Bandit entity;
    ShotgunScene shotgun;
    Vector2 dir;
    Player player;
    CollisionShape2D sprite2D;
    public override void Enter(Entity _entity)
    {
        player = (Player)GetTree().GetFirstNodeInGroup("Player");
        entity = (Bandit)_entity;
        sprite2D = player.GetNode<CollisionShape2D>("PlayerCollisionShape");
        DelayMethod();
    }

    private async void DelayMethod(){
        await Task.Delay(TimeSpan.FromSeconds(2));
        Shit();
    }

    void Shit(){
        entity.shotgun.Shoot(sprite2D.GlobalPosition,BulletTarget.Player);
        DelayMethod();
    }
}
