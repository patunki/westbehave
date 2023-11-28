using Godot;
using System;
using System.ComponentModel.Design;
using System.Dynamic;
using System.Reflection;
using System.Xml.Resolvers;

public partial class player : Entity
{	
	[Export]
	public int moveSpeed { get; set; } = 150;
	Item equipItem;
	public Vector2 heading;
	public Sprite2D sprite;
	private ItemDatabase itemDatabase;
	private GameManager gameManager;
	private AnimationPlayer animationPlayer;
	private string spritePath = "PlayerSprite";
	Inventory inventory;
	private TextureRect equipTexture;
	HungerComponent hungerComponent;
	Node2D Game;


	PackedScene itemScene;
	Node2D itemInstance;
	


	public override void _Ready(){
		sprite = GetNode<Sprite2D>(spritePath);
		itemDatabase = GetNode<ItemDatabase>("/root/ItemDatabase");
		gameManager = GetNode<GameManager>("/root/GameManager");
		animationPlayer = GetNode<AnimationPlayer>("PlayerSprite/AnimationPlayer");
		animationPlayer.Play("IdleAnimation");
		inventory = GD.Load("res://Player/PlayerInventory.tres") as Inventory;
		equipTexture = GetNode<TextureRect>("EquipItem");
		inventory.InventoryChanged += UpdateItem;
		hungerComponent = GetNode<HungerComponent>("%HungerComponent");
		Game = GetNode<Node2D>("/root/Game");
	}

	void UpdateItem(){
		if (inventory.InventoryItems[15] == null){
			equipItem = null;
			if (IsInstanceValid(itemInstance)){
				itemInstance.QueueFree();
			}	
			equipTexture.Texture = null;
		}
		equipItem = inventory.InventoryItems[15];
		if (equipItem != null && equipItem.HAS_SCENE && !IsInstanceValid(itemInstance)){
			itemScene = GD.Load(equipItem.SCENE_PATH) as PackedScene;
			itemInstance = itemScene.Instantiate() as Node2D;
			itemInstance.Call("MyItem",equipItem);
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

	public override void _PhysicsProcess(double delta){

		MoveAndSlide(); //Godot method

	}

	void _on_health_component_health_depleted(){
		GD.Print("ded");
	}


    public override void _Input(InputEvent @event)
    {
        Vector2 inputDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down").Normalized();
		Velocity = inputDirection * moveSpeed;
		if (inputDirection != new Vector2(0,0)){
			heading = inputDirection;
			if (heading == new Vector2(-1,0)){
				sprite.Scale = new Vector2(-1,1);
			}
			if (heading == new Vector2(1,0)){
				sprite.Scale = new Vector2(1,1);
			}
		}

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


/*		itemScene = GD.Load(equipItem.SCENE_PATH) as PackedScene;
		itemInstance = itemScene.Instantiate() as Node2D;
		AddChild(itemInstance);
		itemInstance.QueueFree();

*/
