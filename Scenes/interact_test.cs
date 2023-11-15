using Godot;
using System;

public partial class interact_test : CharacterBody2D
{

    InteractionArea interactionArea;
    Sprite2D sprite2D;

    public override void _Ready()
    {
        interactionArea = GetNode<InteractionArea>("InteractionArea");
        sprite2D = GetNode<Sprite2D>("Sprite2D");
        interactionArea.callable = Callable.From(() => interactionArea.Interact(this, "OnInteract"));

    }

    public void OnInteract(){
        GD.Print("interaktauuuuuuuuuuuus");
    }

}
