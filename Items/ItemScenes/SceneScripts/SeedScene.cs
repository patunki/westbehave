using Godot;
using System;

public partial class SeedScene : Node2D
{
    public Seed seed;
    public Entity entity;
    TextureRect itemTexture;

    public override void _Ready()
    {
        itemTexture = GetNode<TextureRect>("Item");
        GD.Print("ready");
    }

    public void MyItem(Item item, Entity _entity){
        //seed = (Seed)item;
        entity = _entity;
        GD.Print("GotMyItem");
        itemTexture.Texture = seed.ITEM_TEXTURE;
    }
}
