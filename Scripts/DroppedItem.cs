using Godot;
using System;

public partial class DroppedItem : Node2D
{
    [Export]
    public Item item;
    InteractionArea interactionArea;
    public Sprite2D itemTexture;
    Inventory Inventory;

    public override void _Ready()
    {
        interactionArea = GetNode<InteractionArea>("InteractionArea");
        itemTexture = GetNode<Sprite2D>("ItemTexture");
        interactionArea.callable = Callable.From(() => interactionArea.Interact(this, "OnCollect"));
        itemTexture.Texture = item.ITEM_TEXTURE;
        Inventory = GD.Load<Inventory>("res://Player/PlayerInventory.tres");
    }


    void OnCollect(){
        Inventory.AddItem(item,1);
        QueueFree();
    }

}

//player1.GetNode<Item>("PlayerInventory");