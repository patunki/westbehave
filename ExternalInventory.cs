using Godot;
using System;

public partial class ExternalInventory : Control
{
    [Export]
    public Godot.Collections.Array<ItemClass> InventoryItems { get; set; }
    PackedScene inventorySlot;
    GridContainer gridContainer;
    GameManager gameManager;

	public override void _Ready(){
	    inventorySlot = GD.Load<PackedScene>("res://Iventory/InventorySlot.tscn");
	    gridContainer = GetNode<GridContainer>("TextureRect/GridContainer");
        gameManager = GetNode<GameManager>("/root/GameManager");
        //gameManager.ItemLanded += SetItem;
	    //gameManager.ExternalInventory += GetExternal;
        PopulateInv(InventoryItems);
        UpdateInventory();

	}

	public void PopulateInv(Godot.Collections.Array<ItemClass> invSlots){
	    foreach(InventorySlot child in gridContainer.GetChildren()){
	        child.QueueFree();
	    }
	    foreach(ItemClass slotInv in invSlots){
	    	var slot = inventorySlot.Instantiate();
	    	gridContainer.AddChild(slot);
    
		}
	}
        public void UpdateInventory(){
        var slots = gridContainer.GetChildren();
        for (int i = 0; i < InventoryItems.Count; i++){
            if (InventoryItems[i] != null){
                slots[i].Call("Update", InventoryItems[i]);
            }
            else {
                slots[i].Call("Empty");
            }
        }
    }

    
}


