using Godot;
using System;
using System.Collections.Generic;
using System.Linq;


public partial class InventoryScript : Control
{

	bool isOpen;
	[Export]
	InventoryClass inventory;
	PackedScene invetorySlot;
	GridContainer gridContainer;


	public void Close (){
		Visible = false;
		isOpen = false;
	}

	public void Open(){
		Visible = true;
		isOpen = true;

	}

	public void PopulateInv(Godot.Collections.Array<ItemClass> invSlots){
		foreach(InventorySlot child in gridContainer.GetChildren()){
			child.QueueFree();
		}

		foreach(ItemClass slotInv in invSlots){
			var slot = invetorySlot.Instantiate();
			gridContainer.AddChild(slot);
			
			if (slotInv != null){
				slot.Call("Update",slotInv);
				
			}
		}
	}

	public override void _Ready(){
		invetorySlot = GD.Load<PackedScene>("res://Iventory/InventorySlot.tscn");
		gridContainer = GetNode<GridContainer>("TextureRect/GridContainer");

		PopulateInv(inventory.InventoryItems);
		Close();

	}

	public void ToggleInventory()
	{
		if (isOpen){
			Close();
		}
		else {
			Open();
		}   

	}


}
