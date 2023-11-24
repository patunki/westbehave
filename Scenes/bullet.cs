using Godot;
using System;

public partial class bullet : CharacterBody2D
{

	Vector2 velocity;
	[Export]
	int speed = 10;

	public override void _Ready()
	{
		velocity = GetLocalMousePosition();
	}

	public override void _Process(double delta)
	{
		MoveAndCollide(velocity.Normalized() * speed);
	}

	private void _on_timer_timeout(){
			this.QueueFree();
	}


	void _on_area_2d_area_entered(Area2D area){
		if (area is HurtBoxComponent){
			Attack attack = new Attack();
			attack.Damage = 5;
			area.CallDeferred("Damage",attack);
			QueueFree();
		}
	}




}



