using Godot;
using System;

public partial class LootComponent : Node2D
{
    [Export]
    public Godot.Collections.Array<Item> ItemsToDrop { get; set; }
    Node2D game;
    public void DropItems(){
        game = GetNode<Node2D>("/root/Game");
        foreach (Item item in ItemsToDrop){
            PackedScene dropItem = GD.Load("res://Scenes/DroppedItem.tscn") as PackedScene;
            DroppedItem instance = (DroppedItem)dropItem.Instantiate();
            instance.item = item;
            instance.Position = GlobalPosition;
            game.AddChild(instance);
        }


    }
}
