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
    private DragData DragData;
    private Item thisItem;   
    public int index;
    private Inventory inventory;
    bool hasItem;



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
        DragData = gameManager.DragData;

    }



    void _on_gui_input(InputEvent @event){                      //mouse interaction logic
        if (@event is InputEventMouseButton mouseEvent){

            ////Temporary for development
            if (mouseEvent.ButtonIndex == MouseButton.Left && @event.IsPressed() && Input.IsActionPressed("shift")){
                Player player = GetTree().GetFirstNodeInGroup("Player") as Player;
                player.inventory.AddItem(thisItem,1);
                return;
            } 
            ////

            if (thisItem == null){
                hasItem = false;
            } else {
                hasItem = true;
             }

          if (mouseEvent.ButtonIndex == MouseButton.Left && @event.IsPressed()){
 
                switch ((DragData.hasItem,hasItem))
                {
                    case (true, true):
                        if (thisItem.ITEM_ID == DragData.item.ITEM_ID){
                            inventory.InventoryItems[index].ITEM_QUANTITY += DragData.item.ITEM_QUANTITY;
                            DragData.DropDragData();
                        }
                        else {
                            Item temp = thisItem;
                            inventory.InventoryItems[index] = DragData.item;                       
                            Update(DragData.item);
                            DragData.GrabDragData(temp);
                        }
                        //inventory.EmitSignal("InventoryChanged"); 

                        break;

                    case (false, true):
                        DragData.GrabDragData(thisItem);
                        inventory.InventoryItems[index] = null;
                        Empty();
                        //inventory.EmitSignal("InventoryChanged");  
                        break;
                    case (true,false):
                        inventory.InventoryItems[index] = (Item)DragData.item.Duplicate();
                        Update(DragData.DropDragData());
                        //inventory.EmitSignal("InventoryChanged");  
                        break;
                    case (false,false):
                        break;
                    }    

                }

                if (mouseEvent.ButtonIndex == MouseButton.Right && @event.IsPressed()){
                    switch ((DragData.hasItem,hasItem))
                    {
                    case (true, true):
                        if (thisItem.ITEM_ID == DragData.item.ITEM_ID){ //puottaa yhen
                            thisItem.ITEM_QUANTITY--;
                            if (thisItem.ITEM_QUANTITY <= 0){
                                inventory.InventoryItems[index] = null;
                            }
                            DragData.item.ITEM_QUANTITY ++;
                        }
                        //inventory.EmitSignal("InventoryChanged");
                        break;

                    case (false, true):
                        if (thisItem.ITEM_QUANTITY > 1){
                            thisItem.ITEM_QUANTITY -= 1;
                            DragData.GrabDragData((Item)thisItem.Duplicate()); //ottaa yhen;
                            DragData.item.ITEM_QUANTITY = 1;
                            //inventory.EmitSignal("InventoryChanged");
                        }

                        break;
                    case (true,false):
                        if (DragData.item.ITEM_QUANTITY > 1){
                            DragData.item.ITEM_QUANTITY --; //puottaa yhen
                            inventory.InventoryItems[index] = (Item)DragData.item.Duplicate();
                            inventory.InventoryItems[index].ITEM_QUANTITY = 1;
                        }
                        else {
                            inventory.InventoryItems[index] = DragData.DropDragData();
                        }
                        //inventory.EmitSignal("InventoryChanged");
                        break;
                    case (false,false):
                        break;
                    }
                }
        }
    }

    public void GetInventory(Inventory inv){ // itemillä sprtie2d = new
        inventory = inv;
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
    //Updates the invventory visuals. Usually called from InventoryUi.
    public void Update(Item item){
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

