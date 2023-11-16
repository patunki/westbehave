using Godot;
using System;

public partial class GameManager : Node2D
{
    [Signal]
    public delegate void ItemLiftedEventHandler(int index);
    [Signal]
    public delegate void ItemLandedEventHandler(int index);

    public override void _Ready()
    {
    }
}
