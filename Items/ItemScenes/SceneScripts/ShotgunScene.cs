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
    Entity entity;

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
        if (entity is Player){
            radius.LookAt(GetGlobalMousePosition());
            LookMouse();
        }
    }

    public void MyItem(Item item, Entity _entity){
        entity = _entity;
    }

    void LookMouse(){
		Vector2 dir = GlobalPosition.DirectionTo(GetGlobalMousePosition());
		if (dir.X < 0 && Input.IsActionPressed("r_click")){
            shotgunSprite.FlipV = true;
        }else{
            shotgunSprite.FlipV = false;
        }
	}

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("attack") && Input.IsActionPressed("r_click")){
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
        if (Input.IsActionPressed("r_click")){
            LookAt(GetGlobalMousePosition());

        }
        else {
                Rotation = 0;
            }
    }

    public void Shoot(Vector2 dir){
            shotgunSprite.LookAt(dir);
            radius.LookAt(dir);
            Bullet instance = (Bullet)bullet.Instantiate();
            instance.Position = barrel.GlobalPosition;
            instance.velocity = dir;
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
