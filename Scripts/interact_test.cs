using Godot;
using System;

public partial class interact_test : CharacterBody2D
{

    InteractionArea interactionArea;

    public override void _Ready()
    {
        interactionArea = GetNode<InteractionArea>("InteractionArea");
        interactionArea.callable = Callable.From(() => interactionArea.Interact(this, "OnInteract"));

    }

    public void OnInteract(){
        QueueFree();
    }

}
