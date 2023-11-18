using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

//Handles the players inventory spesifically

public partial class InventoryScript : Node2D
{   

    Boolean isOpen;
    Node[] inventorySlot;
    InventoryClass playerInventory;
    ItemDatabase itemDatabase;

    public void UpdateInventory(){
        
        for (int i = 0; i < playerInventory.InventoryItems.Count; i++){
           inventorySlot[i].Call("Update", playerInventory.InventoryItems[i]); //FIX NULL EXEPTION AND ADD CLEAR METHOD!
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
        itemDatabase = GetNode<ItemDatabase>("/root/ItemDatabase");
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
        if (Input.IsActionJustPressed("exit")){
            Close();
        }

    }


}