using Godot;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

//can be used by any inventory
public partial class InventoryClass : Resource
{

    [Export]
	public Godot.Collections.Array<ItemClass> InventoryItems { get; set; }
    [Signal]
    public delegate void InventoryChangedEventHandler();



    //Checks if you have the item and can stack it, if not puts it in the first free spot;
    public bool AddItem(ItemClass itemog, int quant){
        ItemClass item = (ItemClass)itemog.Duplicate();
        EmitSignal(SignalName.InventoryChanged);
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
                    return true;
                }
          
            }
            
        }
        return false;

    }



    
}

