using Godot;
using System;

public partial class EquippedItem : Node2D
{
    [Export]
    Entity entity;
    Node2D Game;
	PackedScene itemScene;
	Node2D itemInstance;
    HungerComponent hungerComponent;
    Inventory inventory;
    Item equipItem;
    TextureRect equipTexture;

    public override void _Ready()
    {
        Game = GetNode<Node2D>("/root/Game");
        hungerComponent = GetNode<HungerComponent>("%HungerComponent");
        inventory = entity.inventory;
        equipTexture = GetNode<TextureRect>("EquipTexture");
        inventory.InventoryChanged += UpdateItem;
    }
    void UpdateItem(){
		if (inventory.InventoryItems[15] == null){
			equipItem = null;
			if (IsInstanceValid(itemInstance)){
				itemInstance.QueueFree();
				entity.EnterFSMState("Idle");
			}	
			equipTexture.Texture = null;
		}
		equipItem = inventory.InventoryItems[15];
		if (equipItem != null && equipItem.HAS_SCENE && !IsInstanceValid(itemInstance)){
			itemScene = GD.Load(equipItem.SCENE_PATH) as PackedScene;
			itemInstance = itemScene.Instantiate() as Node2D;
			itemInstance.Call("MyItem",equipItem,entity);
			AddChild(itemInstance);
			//null the texture
		}
		else if (equipItem != null && !equipItem.HAS_SCENE){
			//GD.Print(equipItem.ITEM_NAME);
			equipTexture.Texture = equipItem.ITEM_TEXTURE;
		}
		else{
			//GD.Print("tyhjennä");
		}
	}

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("interact") && equipItem != null && !equipItem.HAS_SCENE){
			if (equipItem.ITEM_TYPE == ItemType.FOOD){
				Food foodItem = (Food)equipItem;
				int value = foodItem.Eat();
				hungerComponent.FoodAdd(value);
				hungerComponent.WaterAdd(value);

				
			}
			else {
				equipItem.Call("OnInteract", GlobalPosition);
			}

			inventory.NullItemCheck();
			

		}

		if (Input.IsActionJustPressed("drop") && equipItem != null){
                
                PackedScene dropItem = GD.Load("res://Scenes/DroppedItem.tscn") as PackedScene;
                DroppedItem instance = (DroppedItem)dropItem.Instantiate();

                instance.item = equipItem;
                instance.Position = GlobalPosition;
                Game.AddChild(instance);

                equipItem.DecQuant();
                inventory.NullItemCheck();
                
                


        }
    }


}
