using Godot;
using System;

public partial class Bunny : Entity
{
    StateMachine stateMachine;

    public override void _Ready()
    {
        stateMachine = GetNode<StateMachine>("StateMachine");
    }
    public override void _PhysicsProcess(double delta)
    {
        MoveAndSlide();
    }

    void _on_health_component_health_depleted(){
        stateMachine.Transitioned(stateMachine.currentState, "Dead");
    }
}
