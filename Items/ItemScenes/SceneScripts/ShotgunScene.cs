using Godot;
using System;

public partial class ShotgunScene : Node2D
{
    PointLight2D muzzleFlash;
    GpuParticles2D shotParticlesWhite;
    GpuParticles2D shotParticlesRed;
    Node2D radius;
    Marker2D barrel;
    Sprite2D shotgunSprite;
    PackedScene bullet;
    Node2D game;
    AudioStreamPlayer2D audio;

    public override void _Ready()
    {
        shotParticlesWhite = GetNode<GpuParticles2D>("Radius/ShotParticlesWhite");
        shotParticlesRed = GetNode<GpuParticles2D>("Radius/ShotParticlesRed");
        muzzleFlash = GetNode<PointLight2D>("Radius/MuzzleFlash");
        barrel = GetNode<Marker2D>("Radius/Barrel");
        radius = GetNode<Node2D>("Radius");
        shotgunSprite = GetNode<Sprite2D>("ShotgunSprite");
        bullet = GD.Load<PackedScene>("res://Scenes/Bullet.tscn");
        game = GetNode<Node2D>("/root/Game");
        audio = GetNode<AudioStreamPlayer2D>("Gunshot");

    }

    public override void _PhysicsProcess(double delta)
    {
        radius.LookAt(GetGlobalMousePosition());
    }

    public void MyItem(Item item, Entity entity){
        GD.Print(item.ITEM_NAME);
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
            audio.Playing = true;
            Timer timer = new Timer();
            AddChild(timer);
            timer.Start(0.05);
            timer.Timeout += muzzleFlash.Hide;
            timer.Timeout += timer.QueueFree;
        }
    }

}
