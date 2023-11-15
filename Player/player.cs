using Godot;
using System;
using System.ComponentModel.Design;
using System.Dynamic;
using System.Xml.Resolvers;

public partial class player : CharacterBody2D
{	
	[Export]
	public int moveSpeed { get; set; } = 150;
	[Export]
	public InventoryClass playerInventory;
	public Vector2 heading;
	public Sprite2D sprite;
	public TextureRect heldItem;
	public TextureRect hand;
	private InventoryScript inventoryScript;
	private string spritePath = "PlayerSprite";
	


	public override void _Ready(){
		sprite = GetNode<Sprite2D>(spritePath);
		heldItem = GetNode<TextureRect>("Inventory/TextureRect/GridContainer/InventorySlot0/ItemTexture");
		hand = GetNode<TextureRect>("PlayerSprite/HandItem");
		inventoryScript = GetNode<InventoryScript>("Inventory");
		
		
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

		//temporary shooting logiv.
		if (Input.IsActionJustPressed("attack")){
			ItemClass hand = playerInventory.InventoryItems[0];
			if (hand is ItemClass_Weapon){
				ItemClass_Weapon handWep = (ItemClass_Weapon)hand;
				PackedScene bullet = GD.Load<PackedScene>("res://Scenes/bullet.tscn");
				var instance = bullet.Instantiate();
				var barrel = GetNode<Marker2D>("PlayerSprite/Marker2D");
				barrel.AddChild(instance);
				
				GD.Print("pew pew pew");
			}
		}
		

	}

	//Called When an area2D enters HurtBox
	private void _on_hurt_box_area_entered(Area2D area){
		
		if (area.HasMethod("Collect")){ 					//picking up collectibles logic
			area.Call("Collect");
			ItemClass item = (ItemClass)area.Call("Give");
			playerInventory.AddItem(item, item.ITEM_QUANTITY);
			inventoryScript.UpdateInventory();
			
		}
	}

	public override void _PhysicsProcess(double delta){
		if (heldItem.Texture != null){
			hand.Texture = heldItem.Texture; //temp
		}
		else{
			hand.Texture = null;
		}
		GetInput();
		MoveAndSlide(); //Godot method

	}
	
}

//lisää kaupunki ihan missä väkeä
