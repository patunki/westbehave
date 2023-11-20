using Godot;
using System;

public partial class ExternalInventory : Control
{
    [Export]
    public Godot.Collections.Array<ItemClass> InventoryItems { get; set; }
    PackedScene inventorySlot;
    GridContainer gridContainer;
    GameManager gameManager;
    bool isOpen = true;

	public override void _Ready(){
	    inventorySlot = GD.Load<PackedScene>("res://Iventory/ExternalInventorySlot.tscn");
	    gridContainer = GetNode<GridContainer>("TextureRect/GridContainer");
        gameManager = GetNode<GameManager>("/root/GameManager");
        //gameManager.ItemLanded += SetItem;
	    //gameManager.ExternalInventory += GetExternal;
        PopulateInv(InventoryItems);
        UpdateInventory();
        Close();
        

	}


	public void PopulateInv(Godot.Collections.Array<ItemClass> invSlots){
	    foreach(InventorySlot child in gridContainer.GetChildren()){
	        child.QueueFree();
	    }
	    foreach(ItemClass slotInv in invSlots){
	    	var slot = inventorySlot.Instantiate();
            slot.Call("GetInventory",this);
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

	public bool AddItem(ItemClass item, int quant){
        
        GD.Print("Add item called ",item.ITEM_NAME, " quant: ",quant);
        bool hasItem = CheckSame(item, quant);
        if (!hasItem){
            int emptySpot = GetSpot(item,quant);
            InventoryItems[emptySpot] = item;
			UpdateInventory();
            
            return true;
        }
        else if (hasItem){
            return true;
        }else{
            return false;
        }
    }



    private int GetSpot(ItemClass item, int quant){ //Checks for the first empty slot
        
            for (int i = 0; i < InventoryItems.Count; i++){
              if (InventoryItems[i] == null){
                return i;
              }
          
            }

        return -1;
    }

    public bool CheckSame(ItemClass item, int quant){                                                      //checks if you aleady have the item && its stackable
        if (item.IS_STACKABLE) {
            for (int i = 0; i < InventoryItems.Count; i++){
                if (InventoryItems[i] != null && InventoryItems[i].ITEM_ID == item.ITEM_ID){             //if (InventoryItems[i].ITEM_ID == item.ITEM_ID){ ei toimi
                    InventoryItems[i].AddQuant(quant);
					UpdateInventory();
                    return true;
                }
          
            }
            
        }
        return false;

    
	}

}
