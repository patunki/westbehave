using Godot;
using System;

public partial class Tree : StaticBody2D
{
    LootComponent lootComponent;
    bool hasLoot;

    public override void _Ready()
    {
        lootComponent = GetNode<LootComponent>("LootComponent");
        hasLoot = true;
    }

    void _on_health_component_health_depleted(){

        if (hasLoot){
            lootComponent.DropItems();
            lootComponent.QueueFree();
        }

        hasLoot = false;
        Rotation = 90;
    }
}
