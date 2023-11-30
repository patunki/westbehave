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
	[Export]
	Hotbar hotbar;
	bool sceneAlive = false;

    public override void _Ready()
    {
        Game = GetNode<Node2D>("/root/Game");
        hungerComponent = entity.GetNode<HungerComponent>("HungerComponent");
        inventory = entity.inventory;
        equipTexture = GetNode<TextureRect>("EquipTexture");
        inventory.InventoryChanged += UpdateItem;
    }
    public void UpdateItem(){
		int selected = hotbar.selected;
		if (inventory.InventoryItems[selected] == equipItem){
			return;
		}
		if (IsInstanceValid(itemInstance)){	
			itemInstance.QueueFree();
			sceneAlive = false; //Temp
		}
		equipTexture.Texture = null;
		equipItem = inventory.InventoryItems[selected];

		if (equipItem != null && equipItem.HAS_SCENE && sceneAlive == false){
			itemScene = GD.Load(equipItem.SCENE_PATH) as PackedScene;
			try {
				itemInstance = itemScene.Instantiate() as Node2D;
			}catch{
				GD.Print("seceneä ei voitu luoda");
				return;
			}
			itemInstance.Call("MyItem",equipItem,entity);
			AddChild(itemInstance);
			sceneAlive = true;
		}
		else if (equipItem != null && !equipItem.HAS_SCENE){
			equipTexture.Texture = equipItem.ITEM_TEXTURE;

		}
		else{

			GD.Print("tyhjennä ");
		}
	}

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("interact") && equipItem != null && !equipItem.HAS_SCENE){
			if (equipItem.ITEM_TYPE == ItemType.FOOD){
				Food foodItem = (Food)equipItem;
				int value = foodItem.Eat();
				hungerComponent.FoodAdd(value);
				value = foodItem.Drink();
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
                
                


        }
    }

    public override void _Process(double delta)
    {
        inventory.NullItemCheck();
    }


}
