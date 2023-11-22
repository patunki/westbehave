using Godot;
using System;

public partial class Zombie : CharacterBody2D
{
    HealthComponent healthComponent;
    LootComponent lootComponent;

    public override void _Ready()
    {
        healthComponent = GetNode<HealthComponent>("HealthComponent");
        lootComponent = GetNode<LootComponent>("LootComponent");
    }

    void _on_health_component_health_depleted(){
        lootComponent.DropItems();
        QueueFree();
    }

}

//make entity script and all enteties inherit 
