using Godot;
using Godot.NativeInterop;
using System;
using System.Collections.Generic;
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
    

    public Variant MakeData(){
        if (playerInventory.InventoryItems[index] != null){
            var data = playerInventory.InventoryItems[index]; //se mikä nostetaan item
            return data;
        } 
        else{
            return 00001;
        }
        
    }

    public override Variant _GetDragData(Vector2 atPosition)
    {   
        var data = MakeData();
        var previewTexture = new TextureRect();
        previewTexture.Texture = itemTexture.Texture;
        previewTexture.Size = new Vector2(32,32);
        previewTexture.Position = GetGlobalMousePosition();



        SetDragPreview(previewTexture);
        gameManager.EmitSignal(nameof(GameManager.ItemLifted),index);
        
        return data;
    }

    public override bool _CanDropData(Vector2 atPosition, Variant data) //kun tähän yrittää laskea
    {
        if (playerInventory.InventoryItems[index] != null){
            return false;
        }
        else {
            return true;
        }
        

    }

    public override void _DropData(Vector2 atPosition, Variant data) //kun tähän lasketaan
    {
        
        ItemClass dropItem = (ItemClass)data;
        playerInventory.InventoryItems[index] = dropItem; //slotti mihin lasketaan
        gameManager.EmitSignal(nameof(GameManager.ItemLanded),index);
        Update(dropItem);
    }

    


    public override void _Ready(){
        //initialisations
        index = GetIndex();
        itemTexture = GetNode<TextureRect>("ItemTexture");
        playerInventory = (InventoryClass)ResourceLoader.Load("res://Player/PlayerInventory.tres");
        label = GetNode<Label>("Label");
        gameManager = GetNode<GameManager>("/root/GameManager");
        richTextLabel = GetNode<RichTextLabel>("RichTextLabel");
        gameManager.ItemLanded += Landed;
        gameManager.ItemLifted += Lifted;
        richTextLabel.Hide();
        thisItem = playerInventory.InventoryItems[index];
        inventoryScript = GetParent().GetParent().GetParent().GetParent().GetNode<InventoryScript>("Inventory");
        if (thisItem != null){
            richTextLabel.Text = thisItem.HOVER_TEXT;
        }

    }

    void Landed(int sourceIndex){
        InventorySlot slot = GetNode<InventorySlot>("../InventorySlot"+originIndex);
        slot.Delete();
        inventoryScript.UpdateInventory();
    }
    void Lifted(int sourceIndex){
        originIndex = sourceIndex;
    }
    public void Delete(){
        playerInventory.InventoryItems[index] = null;
        itemTexture.Texture = null;
        label.Text = "";
        inventoryScript.UpdateInventory();
    }
    void _on_mouse_entered(){
        if (thisItem != null){
            richTextLabel.Show();
        }
        inventoryScript.UpdateInventory();
        
    }
    void _on_mouse_exited(){
        richTextLabel.Hide();

    }

    public void Update(ItemClass item){
        itemTexture.Texture = item.ITEM_TEXTURE;
        thisItem = item;
        richTextLabel.Text = item.HOVER_TEXT;
        
        if (playerInventory.InventoryItems[index] != null && playerInventory.InventoryItems[index].ITEM_QUANTITY > 1){ //changes label to item quantity
            label.Text = playerInventory.InventoryItems[index].ITEM_QUANTITY.ToString();
        }
        else {
            label.Text = "";
        }
    }




}

