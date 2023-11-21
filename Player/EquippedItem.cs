using Godot;
using System;

public partial class EquippedItem : Node2D
{
    [Export]
    ItemClass item;
    InventoryClass playerInventory;
    TextureRect textureRect;


    public override void _Ready() //kato voiko resurssiin pistää silleen että node.instantiate jolla istten on sen se scripti //kasvin istutus sen perustella mitä on kädessä;
    {   
        playerInventory = GD.Load<InventoryClass>("res://Player/PlayerInventory.tres");
        textureRect = GetNode<TextureRect>("TextureRect");
    }

    public override void _Process(double delta)
    {
        if (playerInventory.InventoryItems[15] != null){
           GetItem();
        }
        else {
            DiscardItem();
        }
    }


    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("interact")){
            if (item != null){
                OnItemInteract();
                
                if (item.ITEM_QUANTITY <= 0){
                        if (item.ITEM_QUANTITY < 0){
                            item.ITEM_QUANTITY = 0;
                        }
                    playerInventory.InventoryItems[15] = null;
                    item = null;
                    textureRect.Texture = null;
                    
                }

                playerInventory.EmitSignal(nameof(InventoryClass.InventoryChanged));
            }
        }

    }

    void OnItemInteract(){
       ItemType itemType = item.ITEM_TYPE;
       string method = item.useName;
       if (item.HasMethod("Plant")){
        item.Call("Plant",GlobalPosition);
       }
       if (item.HasMethod("Plough")){
        item.Call("Plough",GlobalPosition);
       }
       if (item.HasMethod("Eat")){
        item.Call("Eat");
       }
       if (item.HasMethod("UseItem")){
        item.Call("UseItem",GlobalPosition);
       }
       
       //Call(method);

    }
    
    void GetItem(){
            item = playerInventory.InventoryItems[15];
            textureRect.Texture = item.ITEM_TEXTURE;
    }
    
    void DiscardItem(){
            item = null;
            textureRect.Texture = null;
    }


// ITEM USES

   /*void FOOD(){
       ItemClass_Food foodItem = (ItemClass_Food)item;
       GD.Print("Ate ",foodItem.ITEM_NAME, ". +",foodItem.HEALTH_RESTORED,"hp");
       foodItem.DecQuant();
   }

    void ARMOR(){
        ItemClass armorItem = (ItemClass)item; //Make armor class!

   }

   void WEAPON(){
        ItemClass_Weapon weaponItem = (ItemClass_Weapon)item;
        GD.Print(weaponItem.ITEM_NAME);
   }

   void TOOL(){
        ItemClass_Tool toolItem = (ItemClass_Tool)item;
        GD.Print(toolItem.ITEM_NAME);
   }

   void CONSUBLE(){
        ItemClass consumableItem = (ItemClass)item; //Make consumable class!
        GD.Print(consumableItem.ITEM_NAME);
   }

   void MATERIAL(){
        ItemClass materialItem = (ItemClass)item; //Make material class!
        GD.Print(materialItem.ITEM_NAME);
   }

   void MISC(){
        ItemClass miscItem = (ItemClass)item; //Make material class!
        GD.Print(miscItem.ITEM_NAME);
   }

   void SEED(){
        ItemClass_Seed seedItem = (ItemClass_Seed)item;

        if (seedItem.HasMethod("Plant")){
            seedItem.Call("Plant",GlobalPosition);
        }
   }*/




}
