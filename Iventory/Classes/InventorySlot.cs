using Godot;
using Godot.NativeInterop;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

public partial class InventorySlot : Panel
{

    private TextureRect itemTexture;
    private int index;
    private InventoryClass playerInventory;
    private Label label;
    private RichTextLabel richTextLabel;
    private GameManager gameManager;
    private int originIndex;
    private ItemClass thisItem;
    private InventoryScript inventoryScript;
    
    //Checks if this slot has an item, if it does, sotres it in variant data;
    public Variant MakeData(){
        if (playerInventory.InventoryItems[index] != null){
            var data = playerInventory.InventoryItems[index]; 
            return data;
        } 
        else{
            return 00001;
        }
        
    }
    //Triggered when an item is lifted from this slot. Calls MakeData() to get the item, if there is one and stores it in 'data' to be passed fowards.
    public override Variant _GetDragData(Vector2 atPosition)
    {   
        var data = MakeData();

        //gets texture for the drag preview
        var previewTexture = new TextureRect();
        previewTexture.Texture = itemTexture.Texture;
        previewTexture.Size = new Vector2(32,32);
        previewTexture.Position = GetGlobalMousePosition();
        SetDragPreview(previewTexture);

        //Emits a sudo global signal letting other slots know that item has been lifted here
        gameManager.EmitSignal(nameof(GameManager.ItemLifted),index);
        
        return data;
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
        //stores the data in an ItemClass.
        ItemClass dropItem = (ItemClass)data;

        //Modifies the players inventory accordingly and occupies this slot with the item.
        playerInventory.InventoryItems[index] = dropItem;
        //Emits a sudo global signal letting other slots know that an item has landed here.
        gameManager.EmitSignal(nameof(GameManager.ItemLanded),index);
        //Updates the slots visuals with the new item.
        Update(dropItem);
    }

    

    //initialisations
    public override void _Ready(){

        //Gets this slots number into an int;
        index = GetIndex();
        //Gets neccesary nodes and resources.
        itemTexture = GetNode<TextureRect>("ItemTexture");
        playerInventory = (InventoryClass)ResourceLoader.Load("res://Player/PlayerInventory.tres");
        label = GetNode<Label>("Label");
        gameManager = GetNode<GameManager>("/root/GameManager");
        richTextLabel = GetNode<RichTextLabel>("RichTextLabel");
        inventoryScript = GetParent().GetParent().GetParent().GetParent().GetNode<InventoryScript>("Inventory");
        //Connects the signals from GameManager to local methods.
        gameManager.ItemLanded += Landed;
        gameManager.ItemLifted += Lifted;
        //Sets thisItem to the corresponding item in the players inventory.
        thisItem = playerInventory.InventoryItems[index];
        //Initialises the hover text and hides it.
        richTextLabel.Hide();
        if (thisItem != null){
            richTextLabel.Text = thisItem.HOVER_TEXT;
            richTextLabel.Visible = true;
            richTextLabel.Hide();
        }

    }
    //Triggered when a item lands IN ANY SLOT.
    void Landed(int sourceIndex){
        //calls the Delete method on the slot the item was lifted from. This is called from all slots in the players inventory.
        InventorySlot slot = GetNode<InventorySlot>("../InventorySlot"+originIndex);
        slot.Delete();
        //Updates the visuals.
        inventoryScript.UpdateInventory();
    }
    //Triggered when an item is lifted FROM ANY SLOT.
    void Lifted(int sourceIndex){
        //sets the originIndex int to keep track where the item was lifted from.
        originIndex = sourceIndex;
    }
    //Deletes the item from this slot and updates the visuals. Called from another slot with the originIndex reference.
    public void Delete(){
        playerInventory.InventoryItems[index] = null;
        itemTexture.Texture = null;
        label.Text = "";
        inventoryScript.UpdateInventory();
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
        if (item != null){
        itemTexture.Show();     //TEMPORARY
        thisItem = item;
        itemTexture.Texture = item.ITEM_TEXTURE;
        richTextLabel.Text = item.HOVER_TEXT;
        }
        else {
            thisItem = null;
            itemTexture.Hide(); //TEMPORARY, LEAVES THE TEXTURE
        }
        
        if (thisItem != null && thisItem.ITEM_QUANTITY > 1){ //changes label to item quantity
            label.Text = thisItem.ITEM_QUANTITY.ToString();
        }
        else {
            label.Text = "";
        }
    }




}

