using Godot;
using System;

public partial class HotbarSlot : Panel
{
    public TextureRect itemTexture;
    public override void _Ready()
    {
        itemTexture = GetNode<TextureRect>("TextureRect");
    }
    public void Update(Item item){
        if (item != null){
            itemTexture.Texture = item.ITEM_TEXTURE;
        }
        else {
            itemTexture.Texture = null;
        }
    }
}
