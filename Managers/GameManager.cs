using Godot;
using System;

public partial class GameManager : Node2D
{
    [Signal]
    public delegate void ItemLiftedEventHandler(int index);
    [Signal]
    public delegate void ItemLandedEventHandler(int originIndex, int index, ItemClass item); 
    [Signal]
    public delegate void SlotClickedEventHandler(ItemClass item, int quant);
}
