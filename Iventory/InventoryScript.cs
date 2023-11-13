using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class InventoryScript : Node2D
{   

    Boolean isOpen;
    Node[] inventorySlot;
    InventoryClass playerInventory;
    public List<ItemClass> itemClasses = new List<ItemClass>();

    public void UpdateInventory(){
        
        for (int i = 0; i < playerInventory.InventoryItems.Count; i++){
            if (playerInventory.InventoryItems[i] != null){
                inventorySlot[i].Call("Update", playerInventory.InventoryItems[i]);
                GD.Print("Slot",i,": ", playerInventory.InventoryItems[i].ITEM_NAME);
                
            }
            else {
                GD.Print("Slot",i,": ",playerInventory.InventoryItems[i]);
            }
        }
    }
    



    public void Close (){
        Visible = false;
        isOpen = false;
    }

    public void Open(){
        Visible = true;
        isOpen = true;
        UpdateInventory();

    }



    public override void _Ready(){
        playerInventory = (InventoryClass)ResourceLoader.Load("res://Player/PlayerInventory.tres");
        var containerNode = GetNode<GridContainer>("TextureRect/GridContainer");
        inventorySlot = containerNode.GetChildren().ToArray();
        Close();

    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("inventory")){
            if (isOpen){
                Close();
            }
            else {
                Open();
            }
        }       

    }



}