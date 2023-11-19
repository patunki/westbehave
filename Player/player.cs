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
	public Vector2 heading;
	public Sprite2D sprite;

	private InventoryScript inventoryScript;
	private ItemDatabase itemDatabase;
	private GameManager gameManager;
	private AnimationPlayer animationPlayer;
	private string spritePath = "PlayerSprite";
	


	public override void _Ready(){
		sprite = GetNode<Sprite2D>(spritePath);
		inventoryScript = GetNode<InventoryScript>("UI/PlayerInventory");
		itemDatabase = GetNode<ItemDatabase>("/root/ItemDatabase");
		gameManager = GetNode<GameManager>("/root/GameManager");
		animationPlayer = GetNode<AnimationPlayer>("PlayerSprite/AnimationPlayer");

		animationPlayer.Play("IdleAnimation");
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

		if (Input.IsActionJustPressed("interact")){

		}

		if (Input.IsActionJustPressed("drop")){
			
		}
		if (Input.IsActionJustPressed("inventory")){
			inventoryScript.ToggleInventory();
		}

		//temporary shooting logiv.
		/*if (Input.IsActionJustPressed("attack")){
			ItemClass hand = playerInventory.InventoryItems[0];
			if (hand is ItemClass_Weapon){
				ItemClass_Weapon handWep = (ItemClass_Weapon)hand;
				PackedScene bullet = GD.Load<PackedScene>("res://Scenes/bullet.tscn");
				var instance = bullet.Instantiate();
				var barrel = GetNode<Marker2D>("PlayerSprite/Marker2D");
				barrel.AddChild(instance);
				
			}
		}
		*/

	}

	//Called When an area2D enters HurtBox
	private void _on_hurt_box_area_entered(Area2D area){
		


	}

	public override void _PhysicsProcess(double delta){

		GetInput();
		MoveAndSlide(); //Godot method

	}
	
}	
