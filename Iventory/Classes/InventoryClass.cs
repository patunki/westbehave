using Godot;
using System;
using System.Collections.Generic;

//can be used by any inventory
public partial class InventoryClass : Resource
{

    [Export]
	public Godot.Collections.Array<ItemClass> InventoryItems { get; set; }

    public void AddItem(ItemClass item, int quant){ //Checks if you have thi item and can stack it, if not puts it in the first free spot;
        bool hasItem = CheckSame(item, quant);
        int emptySpot = GetSpot(item,quant);
        if (emptySpot > -1 && !hasItem){
            InventoryItems[emptySpot] = item;
        }
    }



    public int GetSpot(ItemClass item, int quant){ //Checks for the first empty slot
        
            for (int i = 0; i < InventoryItems.Count; i++){
              if (InventoryItems[i] == null){
                return i;
              }
          
            }

        return -1;
    }

    private bool CheckSame(ItemClass item, int quant){ //checks if you aleady have the item && its stackable
        if (item.IS_STACKABLE) {
            for (int i = 0; i < InventoryItems.Count; i++){
                if (InventoryItems[i] == item){
                    InventoryItems[i].AddQuant(quant);
                    return true;
                }
          
            }
            
        }
        return false;

    }

    
}

