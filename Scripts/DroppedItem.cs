using Godot;
using System;

public partial class DroppedItem : Node2D
{
    [Export]
    public ItemClass item;
    InteractionArea interactionArea;
    public Sprite2D itemTexture;
    InventoryClass inventoryClass;

    public override void _Ready()
    {
        interactionArea = GetNode<InteractionArea>("InteractionArea");
        itemTexture = GetNode<Sprite2D>("ItemTexture");
        interactionArea.callable = Callable.From(() => interactionArea.Interact(this, "OnCollect"));
        itemTexture.Texture = item.ITEM_TEXTURE;
        inventoryClass = GD.Load<InventoryClass>("res://Player/PlayerInventory.tres");
    }


    void OnCollect(){
        inventoryClass.AddItem(item,1);
        QueueFree();
    }

}

//player1.GetNode<ItemClass>("PlayerInventory");