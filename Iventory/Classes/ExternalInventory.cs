using Godot;
using System;

public partial class ExternalInventory : Control
{
    [Export]
    Storage storage;
    Inventory inventory;
    PackedScene inventorySlot;
    GridContainer gridContainer;
    GameManager gameManager;
    bool isOpen = true;

	public override void _Ready(){
        inventory = storage.inventory;
	    inventorySlot = GD.Load<PackedScene>("res://Iventory/InventorySlot.tscn");
	    gridContainer = GetNode<GridContainer>("TextureRect/GridContainer");
        gameManager = GetNode<GameManager>("/root/GameManager");
        //gameManager.ItemLanded += SetItem;
	    //gameManager.ExternalInventory += GetExternal;
        PopulateInv(inventory.InventoryItems);
        UpdateInventory();
        Close();
        

	}


	public void PopulateInv(Godot.Collections.Array<Item> invSlots){
	    foreach(InventorySlot child in gridContainer.GetChildren()){
	        child.QueueFree();
	    }
	    foreach(Item slotInv in invSlots){
	    	var slot = inventorySlot.Instantiate();
            slot.Call("GetInventory",inventory);
	    	gridContainer.AddChild(slot);
    
		}
	}
        public void UpdateInventory(){
        var slots = gridContainer.GetChildren();
        for (int i = 0; i < inventory.InventoryItems.Count; i++){
            if (inventory.InventoryItems[i] != null){
                slots[i].Call("Update", inventory.InventoryItems[i]);
            }
            else {
                slots[i].Call("Empty");
            }
        }
    }

    public void Close (){
		Visible = false;
		isOpen = false;
	}

	public void Open(){
		Visible = true;
		isOpen = true;

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
