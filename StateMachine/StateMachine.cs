using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node
{

    [Export]
    public State initialState;
    public Godot.Collections.Dictionary<string, State> states = new Godot.Collections.Dictionary<string, State>();
    State currentState;

    public override void _Ready()
    {
        foreach (State child in GetChildren()){
            if (child is State){
                states[child.Name] = child;
            }
        }
        if (initialState != null){
            currentState = initialState; // temp
            currentState.Enter();
            currentState.Transitioned += Transitioned;
        }
    }

    public override void _Process(double delta)
    {
        if (currentState != null){
            currentState.Call("Update",delta);
        }

    }

    public override void _PhysicsProcess(double delta)
    {
        currentState.PhysicsUpdate(delta);
    }

    void Transitioned(State oldState, string newStateName){
        GD.Print("signal caught ", oldState.Name, " -->> ", newStateName);
        if (currentState != oldState){
           return;
        }
        states.TryGetValue(newStateName, out State newState);
        if (currentState != null){
            currentState.Exit();
        }
        
        newState.Enter();
        currentState = newState;
        currentState.Transitioned += Transitioned;
    }


}
