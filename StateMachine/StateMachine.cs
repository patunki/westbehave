using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node
{

    [Export]
    public State initialState;
    public Godot.Collections.Dictionary<string, State> states = new Godot.Collections.Dictionary<string, State>();
    public State currentState;
    [Export]
    Entity entity;
    bool isActive = true;

    public override void _Ready()
    {
        foreach (State child in GetChildren()){
            if (child is State){
                states[child.Name] = child;
            }
        }
        if (initialState != null){
            currentState = initialState; // temp
            currentState.Enter(entity);
            currentState.Transitioned += Transitioned;
        }
    }

    public override void _Process(double delta)
    {
        if (currentState != null){
            currentState.Update(delta);
        }
        if (entity.entityState == EntityState.Dead){
            Deactivate();
        }

    }

    void Deactivate(){
        if (isActive){
            Transitioned(currentState, "Dead");
            isActive = false;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        currentState.PhysicsUpdate(delta);
    }

    public void Transitioned(State oldState, string newStateName){
        if (currentState != oldState){
           return;
        }
        states.TryGetValue(newStateName, out State newState);
        if (currentState != null){
            currentState.Exit();
        }
        
        newState.Enter(entity);
        currentState = newState;
        GD.Print("signal caught ", oldState.Name, " -->> ", newStateName);
        currentState.Transitioned += Transitioned;
    }


}
