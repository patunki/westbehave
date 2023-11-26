using Godot;
using System;

public partial class EquippedItem : Node2D
{
    [Export]
    Item item;
    Inventory playerInventory;
    TextureRect textureRect;
    AnimationPlayer animationPlayer;
    Attack attack;
    player _player;
    Marker2D barrel;
    Sprite2D anims;
    HungerComponent hungerComponent;


    public override void _Ready() //kato voiko resurssiin pistää silleen että node.instantiate jolla istten on sen se scripti //kasvin istutus sen perustella mitä on kädessä;
    {   
        playerInventory = GD.Load<Inventory>("res://Player/PlayerInventory.tres");
        textureRect = GetNode<TextureRect>("TextureRect");
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _player = (player)GetParent();
        barrel = GetNode<Marker2D>("Radius/Marker2D");
        anims = GetNode<Sprite2D>("Anims");
        hungerComponent = GetNode<HungerComponent>("%HungerComponent");


    }

    public override void _PhysicsProcess(double delta)
    {
        if (playerInventory.InventoryItems[15] != null){
           GetItem();
        }
        else {
            DiscardItem();
        }
        if (_player.heading == new Vector2(-1,0)){
            textureRect.Scale = new Vector2(-1,1);
            anims.Scale = new Vector2(-1,1);
        }
		if (_player.heading == new Vector2(1,0)){
			textureRect.Scale = new Vector2(1,1);
            anims.Scale = new Vector2(1,1);
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

                playerInventory.EmitSignal(nameof(Inventory.InventoryChanged));
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
                
                playerInventory.EmitSignal(nameof(Inventory.InventoryChanged));
            
        }

    }



    void OnItemInteract(){
       //ItemType itemType = item.ITEM_TYPE;
       //string method = item.Action;
       if (item.HasMethod("Plant")){
            item.Call("Plant",GlobalPosition);
       }
       if (item.HasMethod("Plough")){
            item.Call("Plough",GlobalPosition);
       }
       if (item.ITEM_TYPE == ItemType.FOOD){
            Food food = (Food)item;
            hungerComponent.FoodAdd(food.Eat());
            hungerComponent.WaterAdd(food.Drink());
       }
       if (item.HasMethod("Water")){
            tool_watering_can can = (tool_watering_can)item;

            if (can.Water(GlobalPosition)){
                animationPlayer.Play("Water");
            }
       }
       if (item.HasMethod("Shoot")){
        

       }
       if (item is tool_axe){
            tool_axe axe = (tool_axe)item;
            attack = axe.Attack();
            animationPlayer.Play("AxeHit");

       }
       

    }
    void _on_hit_area_entered(Area2D area){
        if (area is HurtBoxComponent){
            if (area.GetParent() is Tree){
                area.CallDeferred("Hit",attack);
            }
            else {
                area.CallDeferred("Damage",attack);
            }
        }

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
       Food foodItem = (Food)item;
       GD.Print("Ate ",foodItem.ITEM_NAME, ". +",foodItem.HEALTH_RESTORED,"hp");
       foodItem.DecQuant();
   }

    void ARMOR(){
        Item armorItem = (Item)item; //Make armor class!

   }

   void WEAPON(){
        Weapon weaponItem = (Weapon)item;
        GD.Print(weaponItem.ITEM_NAME);
   }

   void TOOL(){
        Tool toolItem = (Tool)item;
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
        Seed seedItem = (Seed)item;

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




