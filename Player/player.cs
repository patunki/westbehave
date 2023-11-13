using Godot;
using System;

public partial class player : CharacterBody2D
{	
	[Export]
	public int moveSpeed { get; set; } = 150;
	[Export]
	public InventoryClass PlayerInventory;
	public Vector2 heading;
	public Sprite2D sprite;


	public override void _Ready(){
		sprite = GetNode<Sprite2D>("Sprite2D");
		
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

	public override void _PhysicsProcess(double delta){
		GetInput();
		MoveAndSlide();
	}
	
}

