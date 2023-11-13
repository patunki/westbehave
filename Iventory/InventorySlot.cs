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
    private int lastNum;

    public Variant MakeData(){
        var data = playerInventory.InventoryItems[lastNum]; //se mikä nostetaan item
        return data;
    }

    public override Variant _GetDragData(Vector2 atPosition)
    {   
        var data = MakeData();
        TextureRect previewTexture = new TextureRect();
        previewTexture.Texture = itemTexture.Texture;
        SetDragPreview(previewTexture);
        playerInventory.InventoryItems[lastNum] = null;
        itemTexture.Texture = null;
        return data;
    }

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        if (playerInventory.InventoryItems[lastNum] != null){
            return false;
        }
        else {
            return true;
        }
        

    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        
        ItemClass dropItem = (ItemClass)data;
        playerInventory.InventoryItems[lastNum] = dropItem.Copy(); //slotti mihin lasketaan
        Update(dropItem);
    }




    public override void _Ready(){
        String slotName = this.Name;
        char lastChar = slotName[slotName.Length - 1];
        lastNum = lastChar - '0';
        itemTexture = GetNode<TextureRect>("ItemTexture");
        playerInventory = (InventoryClass)ResourceLoader.Load("res://Player/PlayerInventory.tres");

    }

    public void Update(ItemClass item){
        itemTexture.Texture = item.ITEM_TEXTURE;
        
    }




}

