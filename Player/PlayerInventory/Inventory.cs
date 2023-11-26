using Godot;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

//can be used by any inventory
public partial class Inventory : Resource
{

    [Export]
	public Godot.Collections.Array<Item> InventoryItems { get; set; }
    [Signal]
    public delegate void InventoryChangedEventHandler();



    //Checks if you have the item and can stack it, if not puts it in the first free spot;
    public bool AddItem(Item itemog, int quant){
        Item item = (Item)itemog.Duplicate(true);
        GD.Print("Add item called ",item.ITEM_NAME, " quant: ",quant);
        bool hasItem = CheckSame(item, quant);
        if (!hasItem){
            int emptySpot = GetSpot(item,quant);
            InventoryItems[emptySpot] = item;
            EmitSignal(SignalName.InventoryChanged);
            return true;
        }
        else if (hasItem){
            EmitSignal(SignalName.InventoryChanged);
            return true;
        }else{
            return false;
        }
    }

    public void NullItemCheck(){
        for (int i = 0; i < InventoryItems.Count; i++){
            if (InventoryItems[i]?.ITEM_QUANTITY <= 0){
                InventoryItems[i] = null;
                EmitSignal(SignalName.InventoryChanged);
            }
        }
    }



    private int GetSpot(Item item, int quant){ //Checks for the first empty slot
        
            for (int i = 0; i < InventoryItems.Count; i++){
              if (InventoryItems[i] == null){
                return i;
              }
          
            }

        return -1;
    }

    public bool CheckSame(Item item, int quant){                                                      //checks if you aleady have the item && its stackable
        if (item.IS_STACKABLE) {
            for (int i = 0; i < InventoryItems.Count; i++){
                if (InventoryItems[i] != null && InventoryItems[i].ITEM_ID == item.ITEM_ID){             //if (InventoryItems[i].ITEM_ID == item.ITEM_ID){ ei toimi
                    InventoryItems[i].AddQuant(quant);
                    return true;
                }
          
            }
            
        }
        return false;

    }



    
}

