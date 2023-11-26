using Godot;
using System;
using System.ComponentModel.Design;
using System.Dynamic;
using System.Reflection;
using System.Xml.Resolvers;

public partial class player : CharacterBody2D
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


	PackedScene itemScene;
	Node2D itemInstance;
	


	public override void _Ready(){
		sprite = GetNode<Sprite2D>(spritePath);
		itemDatabase = GetNode<ItemDatabase>("/root/ItemDatabase");
		gameManager = GetNode<GameManager>("/root/GameManager");
		animationPlayer = GetNode<AnimationPlayer>("PlayerSprite/AnimationPlayer");
		animationPlayer.Play("IdleAnimation");
		inventory = GD.Load("res://Player/PlayerInventory.tres") as Inventory;
		inventory.InventoryChanged += UpdateItem;
	}

	void UpdateItem(){
		if (inventory.InventoryItems[15] == null){
			equipItem = null;
			if (IsInstanceValid(itemInstance)){
				itemInstance.QueueFree();
			}	
		}
		equipItem = inventory.InventoryItems[15];
		if (equipItem != null && equipItem.HAS_SCENE && !IsInstanceValid(itemInstance)){
			itemScene = GD.Load(equipItem.SCENE_PATH) as PackedScene;
			itemInstance = itemScene.Instantiate() as Node2D;
			itemInstance.Call("MyItem",equipItem);
			AddChild(itemInstance);
		}
		else if (equipItem != null && !equipItem.HAS_SCENE){
			GD.Print(equipItem.ITEM_NAME);
		}
		else{
			GD.Print("tyhjennä");
		}
	}

	public override void _PhysicsProcess(double delta){

		GetInput();
		MoveAndSlide(); //Godot method

	}

	void _on_health_component_health_depleted(){
		GD.Print("ded");
	}
	

	public void GetInput(){
		
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

	}

	//Called When an area2D enters HurtBox
	private void _on_hurt_box_area_entered(Area2D area){
		


	}

	
}	


/*		itemScene = GD.Load(equipItem.SCENE_PATH) as PackedScene;
		itemInstance = itemScene.Instantiate() as Node2D;
		AddChild(itemInstance);
		itemInstance.QueueFree();

*/
