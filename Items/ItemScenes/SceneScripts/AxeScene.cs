using Godot;
using System;

public partial class AxeScene : Node2D
{
    AnimationPlayer animationPlayer;
    Attack attack;

    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        attack = new Attack(); attack.Damage = 10; attack.attackType = AttackType.axe; // temp
    }

    public void MyItem(Item item){

    }

    void _on_hit_area_entered(Area2D area){
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
