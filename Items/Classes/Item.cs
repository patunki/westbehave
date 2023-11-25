using Godot;
using System;
using System.Collections;
using System.ComponentModel;

public enum ItemType
{
    FOOD,
    WEAPON,
    ARMOR,
    TOOL,
    CONSUBLE,
    MATERIAL,
    MISC,
    SEED

}

public partial class Item : Resource
{


    [Export]
    public String ITEM_NAME {get; set;}
    [Export]
    public int ITEM_ID {get; set;}
    [Export]
    public string CustomResourcePath {get; set;}
    [Export]
    public Texture2D ITEM_TEXTURE {get; set;}
    [Export]
    public Boolean IS_STACKABLE {get; set;}
    [Export(PropertyHint.MultilineText)]
    public String HOVER_TEXT {get; set;}
    [Export]
    public int ITEM_QUANTITY {get; set;}
    [Export]
    public int MAX_STACK {get; set;}
    [Export]
    public ItemType ITEM_TYPE = ItemType.MISC;
    [Export]
    public string Action;
    [Export(PropertyHint.MultilineText)]
    public String DevNotes {get; set;}
    
    public Item Copy() => MemberwiseClone() as Item;

    public void AddQuant(int quant){

        ITEM_QUANTITY += quant;

    }

    public int DecQuant(){
        ITEM_QUANTITY--;
        return ITEM_QUANTITY;
    }

    /*public void OnUse(Node2D user){
        EmitSignal(SignalName.ItemInteracted);
        GD.Print("tamaki pyyorii");
    }*/


    //jos id sama nii saa droppaa ja add
}
