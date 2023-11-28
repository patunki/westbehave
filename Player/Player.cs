using Godot;
using System;
using System.ComponentModel.Design;
using System.Dynamic;
using System.Reflection;
using System.Xml.Resolvers;

public partial class Player : Entity
{	
	[Export]
	public int moveSpeed { get; set; } = 150;
	public Vector2 heading;
	public Sprite2D sprite;
	private AnimationPlayer animationPlayer;
	private string spritePath = "PlayerSprite";
	HungerComponent hungerComponent;
	


	public override void _Ready(){
		sprite = GetNode<Sprite2D>(spritePath);
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationPlayer.Play("IdleAnimation");
		hungerComponent = GetNode<HungerComponent>("%HungerComponent");
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

		
    }



	
}	


/*		itemScene = GD.Load(equipItem.SCENE_PATH) as PackedScene;
		itemInstance = itemScene.Instantiate() as Node2D;
		AddChild(itemInstance);
		itemInstance.QueueFree();

*/
