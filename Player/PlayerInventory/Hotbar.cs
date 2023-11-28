using Godot;
using System;

public partial class Hotbar : Control
{
    GridContainer gridContainer;
    [Export]
    Entity entity;
    Inventory inventory;
    

    public override void _Ready()
    {
        gridContainer = GetNode<GridContainer>("ColorRect/GridContainer");
        inventory = entity.inventory;
        inventory.InventoryChanged += UpdateHotbar;
    }

    public void UpdateHotbar(){
        var slots = gridContainer.GetChildren();
        for (int i = 0; i < slots.Count; i++){
            slots[i].Call("Update", inventory.InventoryItems[i]);
        }
    }


}
