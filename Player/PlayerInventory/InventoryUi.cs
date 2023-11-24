using Godot;
using System;
using System.Collections.Generic;
using System.Linq;


public partial class InventoryUi : Control
{

	bool isOpen;
	[Export]
	Inventory inventory;
	PackedScene invetorySlot;
	GridContainer gridContainer;
    GameManager gameManager;


	public void Close (){
		Visible = false;
		isOpen = false;
	}

	public void Open(){
		Visible = true;
		isOpen = true;

	}

	public void PopulateInv(Godot.Collections.Array<Item> invSlots){
		foreach(InventorySlot child in gridContainer.GetChildren()){
			child.QueueFree();
            GD.Print("Deleted:;", child);
		}

		foreach(Item slotInv in invSlots){
			var slot = invetorySlot.Instantiate();
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



	public override void _Ready(){
		invetorySlot = GD.Load<PackedScene>("res://Player/PlayerInventory/InventorySlot.tscn");
		gridContainer = GetNode<GridContainer>("TextureRect/GridContainer");
        gameManager = GetNode<GameManager>("/root/GameManager");
        gameManager.ItemLanded += SetItem;
		gameManager.SlotClicked += GiveItem;
		inventory.InventoryChanged += UpdateInventory;

		PopulateInv(inventory.InventoryItems);
        UpdateInventory();
		Close();

	}


	public void GiveItem(Item item, int quant){
		inventory.AddItem(item, quant);
		UpdateInventory();
	}

    void SetItem(int originIndex, int index, Item item){
        inventory.InventoryItems[originIndex] = null;
        inventory.InventoryItems[index] = item;
        UpdateInventory();
    }

    //void GetExternal(ExternalInventory extInv){

    //}

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("inventory")){
			ToggleInventory();
		}
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
