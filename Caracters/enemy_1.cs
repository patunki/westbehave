using Godot;
using System;

public partial class enemy_1 : CharacterBody2D
{
	[Export]
	public int enemyHealth = 10;
	
	public override void _Ready(){
	
		
	}

	public void TakeDamage(int damage){
		enemyHealth = enemyHealth - damage;
		if (enemyHealth <= 0){
			//kuolee
		}
	}


	public override void _Process(double delta)
	{
		
	}
}
