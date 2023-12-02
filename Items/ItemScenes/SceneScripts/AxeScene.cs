using Godot;
using System;

public partial class AxeScene : ItemScene
{
    AnimationPlayer animationPlayer;
    Attack attack;
    ToolAxe axe;
    Sprite2D axeAnim;

    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        axeAnim = GetNode<Sprite2D>("AxeAnim");
    }

    void LookMouse(){
		Vector2 dir = GlobalPosition.DirectionTo(GetGlobalMousePosition());
		if (dir.X < 0 && Input.IsActionPressed("r_click")){
            axeAnim.FlipV = true;
        }else{
            axeAnim.FlipV = false;
        }
	}

    public override void _Process(double delta)
    {
        LookMouse();
    }

    public override void MyItem(Item item, Entity entity){
        axe = (ToolAxe)item;
    }

    void _on_hit_area_entered(Area2D area){
        attack = axe.Attack();
        if (area is HurtBoxComponent){
            if (area.GetParent() is Tree){
                area.CallDeferred("Hit",attack);
            }
            else {
                area.CallDeferred("Damage",attack);
            }
        }

    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("attack") && Input.IsActionPressed("r_click")){
            animationPlayer.Play("AxeHit");
        }

        if (Input.IsActionPressed("r_click")){
            LookAt(GetGlobalMousePosition());
        }else {
            Rotation = 0;
        }
    }

}
