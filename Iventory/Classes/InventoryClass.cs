using Godot;
using System;

//can be used by any inventory
public partial class InventoryClass : Resource
{

    [Export]
	public Godot.Collections.Array<ItemClass> InventoryItems { get; set; }

   
    
}
