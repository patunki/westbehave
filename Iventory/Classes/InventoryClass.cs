using Godot;
using System;
using System.Collections.Generic;

//can be used by any inventory
public partial class InventoryClass : Resource
{

    [Export]
	public Godot.Collections.Array<ItemClass> InventoryItems {get; set;}
    [Signal]
    public delegate void InventoryChangedEventHandler();
    ItemDatabase itemDatabase;

    

    public bool AddItem(int id, int quant){
        
        if (quant <= 0){
            GD.Print("Cant add less than 1 items at a time");
            return false;
        }
        ItemClass item = itemDatabase.itemDatabase[itemDatabase.GetItem(id)];
            if (item == null){
                GD.Print("no such item in database");
                return false;
            }
        int quantLeft = quant;
        int maxStack;
        if (item.IS_STACKABLE){
            maxStack = item.MAX_STACK;
            for (int i = 0; i < InventoryItems.Count;i++){
               if (quant == 0){
                break;
               }

               ItemClass currentItem = InventoryItems[i];

               if (currentItem.ITEM_ID == item.ITEM_ID){
                    continue;
               }
               if (currentItem.ITEM_QUANTITY < currentItem.MAX_STACK){
                    int originalQuant = currentItem.ITEM_QUANTITY;
                    currentItem.ITEM_QUANTITY = Math.Min(originalQuant + quantLeft, maxStack);
                    quantLeft -= currentItem.ITEM_QUANTITY - originalQuant;
               }
               if (quantLeft == 0){
                return true;
               }
            }
        }
        else {
            maxStack = 1;
        }
        while (quantLeft > 0){
            ItemClass newItem = item;
            newItem.ITEM_QUANTITY = Math.Min(quantLeft,maxStack);

            InventoryItems.Add(newItem);
            quantLeft -= newItem.ITEM_QUANTITY;

        }

        EmitSignal("inventoryChanged", this);
        return true;
    }

    public Variant GetItems(){
        return InventoryItems;
    }
    public ItemClass GetItem(int index){
        return InventoryItems[index];
    }
    public void OnInventoryChanged(){
        EmitSignal("inventoryChanged", this);
    }
    public void SetItems(Variant newItems){

    }

    

    
 


    
}

/*
       //GD.Print(item.ITEM_QUANTITY);
        bool hasItem = CheckSame(item, quant);
        if (!hasItem){
            int emptySpot = GetSpot(item,quant);
            InventoryItems[emptySpot] = item;
            return true;
        }
        else if (hasItem){
            return true;
        }else{
            return false;
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
                if (InventoryItems[i] != null && InventoryItems[i].ITEM_ID == item.ITEM_ID){             //if (InventoryItems[i].ITEM_ID == item.ITEM_ID){ ei toimi
                    GD.Print(InventoryItems[i].ITEM_ID);
                    GD.Print(item.ITEM_ID);
                    InventoryItems[i].AddQuant(quant);
                    return true;
                }
          
            }
            
        }
        return false;

    }
        

        */