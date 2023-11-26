using Godot;
using System;

public partial class WeaponShotgun : Node2D
{
    Weapon shotgun;
    PointLight2D muzzleFlash;
    GpuParticles2D shotParticlesWhite;
    GpuParticles2D shotParticlesRed;
    Node2D radius;
    Marker2D barrel;
    Sprite2D shotgunSprite;
    PackedScene bullet;
    Node2D game;

    public override void _Ready()
    {
        shotgun = GD.Load<Weapon>("res://Items/Repo/weapon_shotgun.tres");
        shotParticlesWhite = GetNode<GpuParticles2D>("Radius/ShotParticlesWhite");
        shotParticlesRed = GetNode<GpuParticles2D>("Radius/ShotParticlesRed");
        muzzleFlash = GetNode<PointLight2D>("Radius/MuzzleFlash");
        barrel = GetNode<Marker2D>("Radius/Barrel");
        radius = GetNode<Node2D>("Radius");
        shotgunSprite = GetNode<Sprite2D>("ShotgunSprite");
        bullet = GD.Load<PackedScene>("res://Scenes/Bullet.tscn");
        game = GetNode<Node2D>("/root/Game");

    }

    public override void _PhysicsProcess(double delta)
    {
        radius.LookAt(GetGlobalMousePosition());
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("attack")){
            Bullet instance = (Bullet)bullet.Instantiate();
            instance.Position = barrel.GlobalPosition;
			game.AddChild(instance);
            shotParticlesRed.Emitting = true;
            shotParticlesWhite.Emitting = true;
            muzzleFlash.Show();
            Timer timer = new Timer();
            AddChild(timer);
            timer.Start(0.05);
            timer.Timeout += muzzleFlash.Hide;
            timer.Timeout += timer.QueueFree;
        }
    }

}
