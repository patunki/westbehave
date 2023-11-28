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

}

//make entity script and all enteties inherit 
