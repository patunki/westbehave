using Godot;
using System;

public partial class LootComponent : Node2D
{
    [Export]
    public Godot.Collections.Array<Item> ItemsToDrop { get; set; }
    public void DropItems(){

        for (int i = 0; i < ItemsToDrop.Count; i++){
            PackedScene dropItem = GD.Load("res://Scenes/DroppedItem.tscn") as PackedScene;
            DroppedItem instance = (DroppedItem)dropItem.Instantiate();
            instance.item = ItemsToDrop[i];
            instance.Position = GlobalPosition;
            GetParent().GetParent().AddChild(instance);
        }


    }
}
