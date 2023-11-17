using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node
{

    [Export]
    public State initialState;
    public Godot.Collections.Dictionary<string, State> states = new Godot.Collections.Dictionary<string, State>();

    public override void _Ready()
    {
        foreach (State child in GetChildren()){
            if (child is State){
                states[child.Name] = child;
            }
        }
    }


}
