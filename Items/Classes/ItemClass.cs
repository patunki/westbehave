using Godot;
using System;
using System.ComponentModel;

public partial class ItemClass : Resource
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
    public int ITEM_COUNT {get; set;}
    [Export]
    public int MAX_STACK {get; set;}
    
    public ItemClass Copy() => MemberwiseClone() as ItemClass;
}
