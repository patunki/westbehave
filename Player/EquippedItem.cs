using Godot;
using System;

public partial class EquippedItem : Node2D
{
    [Export]
    ItemClass item;
    InventoryClass playerInventory;

    public override void _Ready() //kato voiko resurssiin pistää silleen että node.instantiate jolla istten on sen se scripti //kasvin istutus sen perustella mitä on kädessä;
    {   
        playerInventory = GD.Load<InventoryClass>("res://Player/PlayerInventory.tres");
        item.ItemInteracted += OnItemInteract;
    }

    public override void _Process(double delta)
    {
        /*if (playerInventory.InventoryItems[0] != null){
            item = playerInventory.InventoryItems[0];
        }
        if (item != null){
            
        }*/
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("interact")){
            item.Call("OnInteract");
        }
    }

    void OnItemInteract(){
        GD.Print("heloutoimiisignal");
    }


}
