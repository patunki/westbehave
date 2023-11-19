using Godot;
using System;

public partial class GameManager : Node2D
{
    [Signal]
    public delegate void ItemLiftedEventHandler(int index);
    [Signal]
    public delegate void ItemLandedEventHandler(int originIndex, int index, ItemClass item); 


    public override void _Ready()
    {
    }
}
