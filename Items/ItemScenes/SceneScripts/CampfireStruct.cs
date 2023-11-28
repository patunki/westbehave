using Godot;
using System;

public partial class CampfireStruct : Node2D
{   
    CampfireItem campFire;
    Inventory inventory;
    public override void _Ready()
    {
        inventory = GD.Load("res://Player/PlayerInventory.tres") as Inventory;
    }
    public void MyItem(Item item, Entity entity){
        campFire = (CampfireItem)item;
    }
    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("interact")){
            
            Node2D game = GetNode<Node2D>("/root/Game");
            PackedScene camp = GD.Load("res://Scenes/Campfire.tscn") as PackedScene;

            Campfire instance = camp.Instantiate() as Campfire;
            instance.Position = GlobalPosition;
            game.AddChild(instance);
            campFire.DecQuant();
            inventory.NullItemCheck();
        
        }    

    }
}
