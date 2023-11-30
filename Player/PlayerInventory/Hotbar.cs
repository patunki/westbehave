using Godot;
using System;

public partial class Hotbar : Control
{
    GridContainer gridContainer;
    [Export]
    Entity entity;
    //[Export]
    EquippedItem equippedItem;
    Inventory inventory;
    Godot.Collections.Array<Godot.Node> slots;
    public int selected = 0;
    int last = 1;
    

    public override void _Ready()
    {
        gridContainer = GetNode<GridContainer>("ColorRect/GridContainer");
        inventory = entity.inventory;
        inventory.InventoryChanged += UpdateHotbar;
        slots = gridContainer.GetChildren();
        equippedItem = entity.GetNode<EquippedItem>("EquippedItem");
        SelectItem(0);
    }

    public void UpdateHotbar(){
        for (int i = 0; i < slots.Count; i++){
            slots[i].Call("Update", inventory.InventoryItems[i]);
        }
    }

    public void SelectItem(int i){
        
        if (i != last){
            slots[i].Call("Selected");
            slots[last].Call("Deselect");
            equippedItem.UpdateItem();
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("scroll_up")){
            last = selected;
            selected++;
            
            if (selected > 4){
                selected = 4;
            }
            
            SelectItem(selected);
        }
        if (Input.IsActionJustPressed("scroll_down")){
            last = selected;
            selected--;
            
            if (selected < 0){
                selected = 0;
            }
            SelectItem(selected);
        }
        if (Input.IsActionJustPressed("1")){
            last = selected;
            selected = 0;
            SelectItem(selected);
        }
        if (Input.IsActionJustPressed("2")){
            last = selected;
            selected = 1;
            SelectItem(selected);
        }
        if (Input.IsActionJustPressed("3")){
            last = selected;
            selected = 2;
            SelectItem(selected);
        }
        if (Input.IsActionJustPressed("4")){
            last = selected;
            selected = 3;
            SelectItem(selected);
        }
        if (Input.IsActionJustPressed("5")){
            last = selected;
            selected = 4;
            SelectItem(selected);
        }

    }

}
