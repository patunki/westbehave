using Godot;
using System;

public enum WeaponType{
    Shotgun,
    Rifle,
    Pistol,

}

public partial class Weapon : Item
{
    [Export]
    public WeaponType weaponType;
    [Export]
    PackedScene bullet;
    [Export]
    public float cooldown;
    [Export]
    public int bulletCount;
    [Export]
    public int singleBulletDamage;
    [Export]
    public int bulletSpeed;
    [Export]
    public int magazineSize;
    [Export]
    public int bulletsLeft;
    [Export]
    public Item magazine;

}
