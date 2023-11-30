using Godot;
using System;
using System.Collections.Generic;
using System.Linq;


public partial class InventoryUi : Control
{

	bool isOpen;
	[Export]
	Player player;
	PackedScene invetorySlot;
	GridContainer gridContainer;
    GameManager gameManager;
	TextureRect mirror;
	GridContainer equippables;


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
			slot.Call("GetInventory",player.inventory);
		}
	}

    public void UpdateInventory(){
		
        var slots = gridContainer.GetChildren();
        for (int i = 0; i < player.inventory.InventoryItems.Count; i++){
            if (player.inventory.InventoryItems[i] != null){
                slots[i].Call("Update", player.inventory.InventoryItems[i]);
            }
            else {
                slots[i].Call("Empty");
            }
        }
		
    }




	public override void _Ready(){
		equippables = GetNode<GridContainer>("TextureRect/Equippables");
		invetorySlot = GD.Load<PackedScene>("res://Iventory/InventorySlot.tscn");
		gridContainer = GetNode<GridContainer>("TextureRect/GridContainer");
        gameManager = GetNode<GameManager>("/root/GameManager");
		mirror = GetNode<TextureRect>("Mirror");
        //gameManager.ItemLanded += inventory.EmitChanged;
		gameManager.SlotClicked += GiveItem;
		player.inventory.InventoryChanged += UpdateInventory;
		Sprite2D test = player.GetNode<Sprite2D>("MirrorSprite");
		mirror.Texture = test.Texture;
		PopulateInv(player.inventory.InventoryItems);
        UpdateInventory();
		Close();

	}


	public void GiveItem(Item item){
		player.inventory.AddItem(item, 1);
		UpdateInventory();
	}

    void SetItem(int originIndex, int index, Item item){
        player.inventory.InventoryItems[originIndex] = null;
        player.inventory.InventoryItems[index] = item;
        //UpdateInventory();
		player.inventory.EmitChange();
    }

    //void GetExternal(ExternalInventory extInv){

    //}

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("inventory")){
			ToggleInventory();
		}
		if (Input.IsActionJustPressed("exit")){
			Close();
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
