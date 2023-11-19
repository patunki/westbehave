using Godot;
using System;


public partial class InteractionArea : Area2D
{
    [Export]
    public string actionName = "interact";
    [Signal]
    public delegate void PlayerExitedEventHandler();
    private InteractionManager interactionManager;
    public Callable callable;
    


    public override void _Ready()
    {
        interactionManager = GetNode<InteractionManager>("/root/InteractionManager");
    }

    public void Interact(Node node, StringName name){
        node.Call(name);
    }

    void _on_body_entered(Node2D body){
        if (body.IsInGroup("Player")){
            interactionManager.RegisterArea(this);
        }
    }
    void _on_body_exited(Node2D body){
        if (body.IsInGroup("Player")){
            interactionManager.UnregisterArea(this);
            EmitSignal(SignalName.PlayerExited);
        }
        

    }
}
