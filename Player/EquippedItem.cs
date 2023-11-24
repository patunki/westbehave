using Godot;
using System;

public partial class EquippedItem : Node2D
{
    [Export]
    Item item;
    InventoryClass playerInventory;
    TextureRect textureRect;
    AnimationPlayer animationPlayer;


    public override void _Ready() //kato voiko resurssiin pistää silleen että node.instantiate jolla istten on sen se scripti //kasvin istutus sen perustella mitä on kädessä;
    {   
        playerInventory = GD.Load<InventoryClass>("res://Player/PlayerInventory.tres");
        textureRect = GetNode<TextureRect>("TextureRect");
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
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
                    
                    item.ITEM_QUANTITY = 1;
                    playerInventory.InventoryItems[15] = null;
                    item = null;
                    textureRect.Texture = null;
                    
                }

                playerInventory.EmitSignal(nameof(InventoryClass.InventoryChanged));
            }
        }

        if (Input.IsActionJustPressed("drop") && item != null){
                
                PackedScene dropItem = GD.Load("res://Scenes/DroppedItem.tscn") as PackedScene;
                DroppedItem instance = (DroppedItem)dropItem.Instantiate();

                instance.item = item;
                instance.Position = GlobalPosition;
                GetParent().GetParent().GetParent().AddChild(instance);

                item.DecQuant();
                if (item.ITEM_QUANTITY <= 0){
                    
                    item.ITEM_QUANTITY = 1;
                    playerInventory.InventoryItems[15] = null;
                    item = null;
                    textureRect.Texture = null;
                    
                }
                
                playerInventory.EmitSignal(nameof(InventoryClass.InventoryChanged));
            
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
       if (item.HasMethod("Water")){
        item.Call("Water",GlobalPosition);
        animationPlayer.Play("kastelu");

       }
       if (item.HasMethod("Shoot")){
        
				PackedScene bullet = GD.Load<PackedScene>("res://Scenes/bullet.tscn");
				var instance = bullet.Instantiate();
				var barrel = GetNode<Marker2D>("Marker2D");
				barrel.AddChild(instance);

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

}
// ITEM USES

   /*void FOOD(){
       Item_Food foodItem = (Item_Food)item;
       GD.Print("Ate ",foodItem.ITEM_NAME, ". +",foodItem.HEALTH_RESTORED,"hp");
       foodItem.DecQuant();
   }

    void ARMOR(){
        Item armorItem = (Item)item; //Make armor class!

   }

   void WEAPON(){
        Item_Weapon weaponItem = (Item_Weapon)item;
        GD.Print(weaponItem.ITEM_NAME);
   }

   void TOOL(){
        Item_Tool toolItem = (Item_Tool)item;
        GD.Print(toolItem.ITEM_NAME);
   }

   void CONSUBLE(){
        Item consumableItem = (Item)item; //Make consumable class!
        GD.Print(consumableItem.ITEM_NAME);
   }

   void MATERIAL(){
        Item materialItem = (Item)item; //Make material class!
        GD.Print(materialItem.ITEM_NAME);
   }

   void MISC(){
        Item miscItem = (Item)item; //Make material class!
        GD.Print(miscItem.ITEM_NAME);
   }

   void SEED(){
        Item_Seed seedItem = (Item_Seed)item;

        if (seedItem.HasMethod("Plant")){
            seedItem.Call("Plant",GlobalPosition);
        }
   }*/
               /* DroppedItem droppedItem = new DroppedItem();
                droppedItem.item = item;
                InteractionArea interactionArea = new InteractionArea();
                CollisionShape2D coller = new CollisionShape2D();
                CircleShape2D circle = new CircleShape2D();
                interactionArea.Name = "InteractionArea";
                circle.Radius = 32;
                coller.Shape = circle;
                interactionArea.AddChild(coller);
                interactionArea.actionName = "TO COLLECT";
                Sprite2D sprite2D = new Sprite2D();
                sprite2D.Name = "ItemTexture";
                droppedItem.AddChild(interactionArea);
                droppedItem.AddChild(sprite2D);
                droppedItem.Position = GlobalPosition;
                GetParent().GetParent().GetParent().AddChild(droppedItem);*/




