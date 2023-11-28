using Godot;
using System;

public partial class HotbarSlot : Panel
{
    public TextureRect itemTexture;
    ColorRect colorRect;
    Label label;

    public override void _Ready()
    {
        label = GetNode<Label>("Label");
        itemTexture = GetNode<TextureRect>("TextureRect");
        colorRect = GetNode<ColorRect>("ColorRect");
    }
    public void Update(Item item){
        if (item != null){
            itemTexture.Texture = item.ITEM_TEXTURE;
            label.Text = item.ITEM_QUANTITY.ToString();
        }
        else {
            itemTexture.Texture = null;
            label.Text = "";
        }
    }
    public void Selected(){
        colorRect.Show();
    }

        public void Deselect(){
        colorRect.Hide();
    }
}
