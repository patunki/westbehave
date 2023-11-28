using Godot;
using System;

public partial class Zombie : Entity
{
    HealthComponent healthComponent;
    LootComponent lootComponent;
    Sprite2D sprite;

    public override void _Ready()
    {
        healthComponent = GetNode<HealthComponent>("HealthComponent");
        lootComponent = GetNode<LootComponent>("LootComponent");
        sprite = GetNode<Sprite2D>("Sprite");
    }

    void _on_health_component_health_depleted(){
        lootComponent.DropItems();
        QueueFree();
    }
    public override void _PhysicsProcess(double delta)
    {
        MoveAndSlide();
        if (Velocity.X > 0){
            sprite.FlipH = true;
        }
        else {
            sprite.FlipH = false;
        }

    }

    void _on_fist_area_entered(Area2D area){
        Attack attack = new Attack(); attack.Damage = 8;
        if (area is HurtBoxComponent){
            area.Call("Damage",attack);
        }
    }

}

//make entity script and all enteties inherit 
