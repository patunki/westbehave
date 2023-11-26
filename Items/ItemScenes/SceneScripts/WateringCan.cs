using Godot;
using System;

public partial class WateringCan : Node2D
{
    public override void _Ready()
    {
        
    }

    public void MyItem(Item item){
        GD.Print(item.ITEM_NAME);
    }
}