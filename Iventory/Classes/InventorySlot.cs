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

    //initialisations
    public override void _Ready(){

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

        itemTexture.Show();     //TEMPORARY
        thisItem = item;
        itemTexture.Texture = item.ITEM_TEXTURE;
        richTextLabel.Text = item.HOVER_TEXT;
   
        if (thisItem.ITEM_QUANTITY > 1 && thisItem.IS_STACKABLE){ //changes label to item quantity
            label.Text = thisItem.ITEM_QUANTITY.ToString();
        }
        else {
            label.Text = "";
        }
    }




}

