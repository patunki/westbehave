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




}



