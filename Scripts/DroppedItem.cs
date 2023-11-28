using Godot;
using System;

public partial class DroppedItem : Node2D
{
    [Export]
    public Item item;
    InteractionArea interactionArea;
    public Sprite2D itemTexture;
    Inventory inventory;
    Entity entity;

    public override void _Ready()
    {
        interactionArea = GetNode<InteractionArea>("InteractionArea");
        itemTexture = GetNode<Sprite2D>("ItemTexture");
        interactionArea.callable = Callable.From(() => interactionArea.Interact(this, "OnCollect"));
        itemTexture.Texture = item.ITEM_TEXTURE;
        interactionArea.PlayerEntered += GetEntity;
        
    }

    void GetEntity(Node2D body){
        entity = body as Entity;
        inventory = entity.inventory;
    }


    void OnCollect(){
        inventory.AddItem(item,1);
        QueueFree();
    }

}

//player1.GetNode<Item>("PlayerInventory");