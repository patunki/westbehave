using Godot;
using System;

public partial class AxeScene : Node2D
{
    AnimationPlayer animationPlayer;
    Attack attack;
    ToolAxe axe;

    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        
    }

    public void MyItem(Item item, Entity entity){
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
        if (Input.IsActionJustPressed("attack")){
            animationPlayer.Play("AxeHit");
        }
    }

}
