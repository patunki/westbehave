using Godot;
using System;

public partial class EquippedItem : Node2D
{
    [Export]
    Entity entity;
    Node2D Game;
	PackedScene packedScene;
	ItemScene itemInstance;
    HungerComponent hungerComponent;
    Inventory inventory;
    public Item equipItem;
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
			packedScene = GD.Load(equipItem.SCENE_PATH) as PackedScene;
			try {
				itemInstance = packedScene.Instantiate() as ItemScene;
			}catch{
				GD.Print("seceneä ei voitu luoda");
				return;
			}
			itemInstance.MyItem(equipItem,entity);
			AddChild(itemInstance);
			sceneAlive = true;
		}
		else if (equipItem != null && !equipItem.HAS_SCENE){
			equipTexture.Texture = equipItem.ITEM_TEXTURE;

		}
		
	}

    public override void _Input(InputEvent @event)
    {
		if (equipItem != null && !equipItem.HAS_SCENE && Input.IsActionJustPressed("interact")){
			equipItem.Call("OnInteract",GlobalPosition);
		}

		if (Input.IsActionJustPressed("drop") && equipItem != null){
                
                PackedScene dropItem = GD.Load("res://Scenes/DroppedItem.tscn") as PackedScene;
                DroppedItem instance = (DroppedItem)dropItem.Instantiate();

                instance.item = equipItem.Duplicate() as Item;
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
