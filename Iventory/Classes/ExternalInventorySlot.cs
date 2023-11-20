using Godot;
using Godot.NativeInterop;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

public partial class ExternalInventorySlot : Panel
{
    [Signal]
    public delegate  void SlotClickedEventHandler(ItemClass item);
    private TextureRect itemTexture;
    private Label label;
    private RichTextLabel richTextLabel;
    private GameManager gameManager;
    private ItemClass thisItem;   
    public int index;
    private int originIndex;
    private ExternalInventory externalInventory;


    

    //initialisations
    public override void _Ready(){
        index = GetIndex();
        //Gets neccesary nodes and resources.
        itemTexture = GetNode<TextureRect>("ItemTexture");
        label = GetNode<Label>("Label");
        gameManager = GetNode<GameManager>("/root/GameManager");
        richTextLabel = GetNode<RichTextLabel>("RichTextLabel");
        //Initialises the hover text and hides it.
        richTextLabel.Hide();
        if (thisItem != null){
            richTextLabel.Text = thisItem.HOVER_TEXT;
            richTextLabel.Visible = true;
            richTextLabel.Hide();
        }

    }



    void _on_gui_input(InputEvent @event){
        if (@event is InputEventMouseButton mouseEvent && thisItem != null){
          if (mouseEvent.ButtonIndex == MouseButton.Left && @event.IsPressed() || mouseEvent.ButtonIndex == MouseButton.Right && @event.IsPressed()){

            gameManager.EmitSignal(nameof(SlotClicked),thisItem,thisItem.ITEM_QUANTITY);

          }
           
        }
    }
    public void GetInventory(ExternalInventory inv){
        externalInventory = inv;
    }

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

    /*public override bool _CanDropData(Vector2 atPosition, Variant data)
    {   
        ItemClass dropItem = (ItemClass)data;
        if (thisItem == null || dropItem.ITEM_ID == thisItem.ITEM_ID){
            return true;
        }
        else {
            return false;
        }
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        ItemClass dropItem = (ItemClass)data;
        externalInventory.AddItem(dropItem, dropItem.ITEM_QUANTITY);
    }*/

    //Updates the invventory visuals. Usually called from InventoryScript.
    public void Update(ItemClass item){
        thisItem = item.Copy();
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

