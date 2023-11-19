using Godot;
using System;


public partial class Chest : StaticBody2D
{
    public bool isOpen = false;
    private Sprite2D spriteClosed;
    private Sprite2D spriteOpen;
    InteractionArea interactionArea;
    ExternalInventory externalInventory;


    public override void _Ready()
    {   
        externalInventory = GetNode<ExternalInventory>("ExternalInventory");
        interactionArea = GetNode<InteractionArea>("InteractionArea");
        interactionArea.callable = Callable.From(() => interactionArea.Interact(this, "OnInteract"));
        spriteClosed = GetNode<Sprite2D>("ChestClosed");
        spriteOpen = GetNode<Sprite2D>("ChestOpen");

    }

    public void OnInteract(){
        if (isOpen){
            Close();
        } else {
            Open();
        }

        externalInventory.ToggleInventory();


    }

    public void Close(){
        spriteOpen.Hide();
        isOpen = false;
    }
    public void Open(){
        spriteOpen.Show();
        isOpen = true;
    }

}
