using Godot;
using System;

public partial class ShotgunScene : Node2D
{
    PointLight2D muzzleFlash;
    GpuParticles2D shotParticlesWhite;
    GpuParticles2D shotParticlesRed;
    Node2D radius;
    Marker2D barrel;
    Sprite2D weaponSprite;
    PackedScene bullet;
    Node2D game;
    AudioStreamPlayer2D audio;
    Entity entity;
    Weapon weapon;
    Timer cooldown;

    bool canShoot = false;


    public override void _Ready()
    {
        shotParticlesWhite = GetNode<GpuParticles2D>("Radius/ShotParticlesWhite");
        shotParticlesRed = GetNode<GpuParticles2D>("Radius/ShotParticlesRed");
        muzzleFlash = GetNode<PointLight2D>("Radius/MuzzleFlash");
        barrel = GetNode<Marker2D>("Radius/Barrel");
        radius = GetNode<Node2D>("Radius");
        weaponSprite = GetNode<Sprite2D>("WeaponSprite");
        bullet = GD.Load<PackedScene>("res://Scenes/Bullet.tscn");
        game = GetNode<Node2D>("/root/Game");
        audio = GetNode<AudioStreamPlayer2D>("Gunshot");
        cooldown = GetNode<Timer>("Cooldown");
        canShoot = true;

    }

    public override void _PhysicsProcess(double delta)
    {
        if (entity is Player){
            radius.LookAt(GetGlobalMousePosition());
            GetInput();
            LookMouse();
        }
    }

    public void MyItem(Item item, Entity _entity){
        entity = _entity;
        weapon = (Weapon)item;
        if (weaponSprite == null){
            weaponSprite = GetNode<Sprite2D>("WeaponSprite");
        }
        weaponSprite.Texture = weapon.ITEM_TEXTURE;
        
    }

    void LookMouse(){
		Vector2 dir = GlobalPosition.DirectionTo(GetGlobalMousePosition());
		if (dir.X < 0 && Input.IsActionPressed("r_click")){
            weaponSprite.FlipV = true;
        }else{
            weaponSprite.FlipV = false;
        }
	}

    public void GetInput()
    {
        if (Input.IsActionJustPressed("attack") && Input.IsActionPressed("r_click") && entity is Player){
            Shoot(GetGlobalMousePosition(),BulletTarget.Mouse);
        }
        if (Input.IsActionPressed("r_click") && entity is Player){
            LookAt(GetGlobalMousePosition());

        }
        else {
                Rotation = 0;
            }
    }

    public void Shoot(Vector2 dir, BulletTarget target){ //for Ai
        if (canShoot){
            weaponSprite.LookAt(dir);
            radius.LookAt(dir);
            Bullet instance = (Bullet)bullet.Instantiate();
            instance.target = target;
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

    void Reloaded(){
        canShoot = true;
    }

}
