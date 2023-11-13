using Godot;
using System;

public partial class InventoryClass : Resource
{

    [Export]
	public Godot.Collections.Array<ItemClass> InventoryItems { get; set; }

   
    
}
