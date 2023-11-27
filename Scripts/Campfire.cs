using Godot;
using System;

public partial class Campfire : StaticBody2D
{
    Sprite2D outSprite;
    Sprite2D litSprite;
    AnimationPlayer animationPlayer;
    InteractionArea interactionArea;
    Inventory inventory;
    bool isLit = false;

    public override void _Ready()
    {
        outSprite = GetNode<Sprite2D>("Out");
        litSprite = GetNode<Sprite2D>("Lit");
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        interactionArea = GetNode<InteractionArea>("InteractionArea");
        interactionArea.callable = Callable.From(() => interactionArea.Interact(this, "OnInteract"));
        inventory = GD.Load("res://Player/PlayerInventory.tres") as Inventory;
    }

    void OnInteract(){
        Toggle();
        
    }
    
    void Toggle(){
        if (!isLit){
            foreach (Item item in inventory.InventoryItems){
                if (item != null && item.ITEM_NAME == "Lighter"){
                    isLit = true;
                    animationPlayer.Play("Lit");
                }
            }

        }
        else{
            animationPlayer.Play("RESET");
            isLit = false;
        }
    }
}
