using Godot;
using Godot.NativeInterop;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

public partial class InventorySlot : Panel
{

    private TextureRect itemTexture;
    private Label label;
    private RichTextLabel richTextLabel;
    private GameManager gameManager;
    private ItemClass thisItem;   
    public int index;
    private int originIndex;


     public override Variant _GetDragData(Vector2 atPosition)
    {   
        if (thisItem == null){
            string noItem = "NO ITEM IN THIS SLOT.";
            return noItem;
        }
        var previewTexture = new TextureRect();
        previewTexture.Texture = thisItem.ITEM_TEXTURE;
        SetDragPreview(previewTexture);
        gameManager.EmitSignal(nameof(GameManager.ItemLifted),index);
        return thisItem;
    }

    //Triggered when a dragged item is hovered above this slot. Data is the item that is being dragged. If it returns false, item will not be dropped.
    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        if (thisItem != null){
            return false;
        }
        else {
            return true;
        }
        

    }

    //Triggered when an item is dropped into this slot.
    public override void _DropData(Vector2 atPosition, Variant data)
    {   
        ItemClass dropItem = (ItemClass)data;
        gameManager.EmitSignal(nameof(GameManager.ItemLanded),originIndex, index, dropItem);

    }

    //initialisations
    public override void _Ready(){
        index = GetIndex();
        //Gets neccesary nodes and resources.
        itemTexture = GetNode<TextureRect>("ItemTexture");
        label = GetNode<Label>("Label");
        gameManager = GetNode<GameManager>("/root/GameManager");
        richTextLabel = GetNode<RichTextLabel>("RichTextLabel");
        gameManager.ItemLifted += GetOriginIndex;
        //Initialises the hover text and hides it.
        richTextLabel.Hide();
        if (thisItem != null){
            richTextLabel.Text = thisItem.HOVER_TEXT;
            richTextLabel.Visible = true;
            richTextLabel.Hide();
        }

    }

    public void GetOriginIndex(int source){
        originIndex = source;
    }

    /*void _on_gui_input(InputEvent @event){
        if (@event is InputEventMouseButton mouseEvent){
          if (mouseEvent.ButtonIndex == MouseButton.Left && @event.IsPressed() || mouseEvent.ButtonIndex == MouseButton.Right && @event.IsPressed()){

            GD.Print("KLICKED  ", mouseEvent.ButtonIndex,"  ", GetIndex());

          }
           
        }
    }*/

    //When mouse is hovered here.
    void _on_mouse_entered(){
        
        if (thisItem != null){
            richTextLabel.Show();
        }
        else {
            richTextLabel.Visible = false;
        }
        
    }
    //When mouse leaves the hover area.
    void _on_mouse_exited(){
        richTextLabel.Hide();

    }
    //Updates the invventory visuals. Usually called from InventoryScript.
    public void Update(ItemClass item){

        thisItem = item;
        itemTexture.Texture = item.ITEM_TEXTURE;
        richTextLabel.Text = item.HOVER_TEXT;
        itemTexture.Show();
   
        if (thisItem.ITEM_QUANTITY > 1 && thisItem.IS_STACKABLE){ //changes label to item quantity
            label.Text = thisItem.ITEM_QUANTITY.ToString();
        }
        else {
            label.Text = "";
        }
    }
    public void Empty(){
        thisItem = null;
        itemTexture.Texture = null;
        itemTexture.Hide();
        label.Text = "";
    }



}

