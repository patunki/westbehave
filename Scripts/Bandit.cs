using Godot;
using System;

public partial class Bandit : Entity
{
    [Export]
    public ShotgunScene shotgun;
    [Export]
    Item item;
    StateMachine stateMachine;

    public override void _Ready()
    {
        shotgun.MyItem(item,this);
        stateMachine = GetNode<StateMachine>("StateMachine");
    }
    public override void _PhysicsProcess(double delta)
    {
        MoveAndSlide();
    }

    void _on_health_component_health_depleted(){
        Die();
        stateMachine.Transitioned(null, "Dead");
    }

}
