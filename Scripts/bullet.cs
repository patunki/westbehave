using Godot;
using System;

public partial class bullet : CharacterBody2D
{

	Vector2 velocity;
	[Export]
	int speed = 10;
    public override void _Ready()
    {
    }

    public override void _PhysicsProcess(double delta)
	{
		MoveAndCollide(velocity.Normalized() * speed * (int)delta);
	}

	private void _on_timer_timeout(){
			QueueFree();
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



