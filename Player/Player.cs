using Godot;



public partial class Player : Entity
{	
	[Export]
	public int moveSpeed { get; set; } = 150;
	[Export]
	public Godot.Collections.Array<Item> wornItems { get; set; } = new Godot.Collections.Array<Item>();
	public Vector2 heading;
	public Sprite2D sprite;
	private AnimationPlayer animationPlayer;
	private string spritePath = "PlayerSprite";
	public HungerComponent hungerComponent;
	StateMachine stateMachine;
	
	


	public override void _Ready(){
		sprite = GetNode<Sprite2D>(spritePath);
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		hungerComponent = GetNode<HungerComponent>("HungerComponent");
		stateMachine = GetNode<StateMachine>("StateMachine");
		
	}


	public override void _PhysicsProcess(double delta){

		if (canMove){
			MoveAndSlide();
		}
		LookMouse();
		GD.Print("P: ",GlobalPosition);

	}

	void LookMouse(){
		Vector2 dir = GlobalPosition.DirectionTo(GetGlobalMousePosition());
		if (dir.Y+0.2 < 0 && Input.IsActionPressed("r_click")){
            animationPlayer.Play("IdleUp");
        }else{
            animationPlayer.Play("IdleAnimation");
        }
		
	}

	void _on_health_component_health_depleted(){
		Die();
		Modulate = new Color (1,0,0);
		Label label = new Label();
		label.Text = "DEAD";
		AddChild(label);
		animationPlayer.Pause();
	}

	void _on_health_component_damage_taken(){
		sprite.Modulate = new Color(1,0,0);
		Timer timer = new Timer();
		AddChild(timer);
		timer.Start(0.2);
		timer.Timeout += ResetColor;
		timer.Timeout += timer.QueueFree;
	}

	void ResetColor(){
		sprite.Modulate = new Color(1,1,1);
	}

    public override void EnterFSMState(string stateName)
    {
        stateMachine.Transitioned(stateMachine.currentState, stateName);
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
