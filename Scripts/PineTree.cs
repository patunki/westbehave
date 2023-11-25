using Godot;
using System;

public partial class PineTree : Tree
{
    AnimationPlayer animationPlayer;
    LootComponent lootComponent;
    bool hasLoot;

    public override void _Ready()
    {   
        
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        lootComponent = GetNode<LootComponent>("LootComponent");
        hasLoot = true;
    }

    void _on_health_component_health_depleted(){
        if (hasLoot){
            lootComponent.DropItems();
            lootComponent.QueueFree();
            animationPlayer.Play("PineFall");
        }
        hasLoot = false;
        Timer timer = new Timer();
        AddChild(timer);
        timer.Start(10);
        timer.Timeout += QueueFree;
    }


}
