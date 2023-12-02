using Godot;
using System;

public partial class CampfireStruct : ItemScene
{   
    Structure campFire;
    Inventory inventory;
    Entity entity;

    public override void MyItem(Item item, Entity _entity){
        campFire = (Structure)item;
        entity = _entity;
        inventory = entity.inventory;
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
