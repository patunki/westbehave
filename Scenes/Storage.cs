using Godot;
using System;

public partial class Storage : StaticBody2D
{
    [Export]
    public Inventory inventory = new Inventory();
}
