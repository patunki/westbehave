using Godot;
using System;

public partial class DragData : Node2D
{
	TextureRect textureRect;
	Label label;
	public Item item;
	public bool hasItem;
	

	public override void _Ready()
	{
		textureRect = GetNode<TextureRect>("TextureRect");
		label = GetNode<Label>("Label");
		
	}

	public override void _Process(double delta)
	{
		if (hasItem){
			Show();
			if (item.ITEM_QUANTITY > 1){
				label.Text = item.ITEM_QUANTITY.ToString();
			}else{
				label.Text = "";
			}
			GD.Print(item.ITEM_QUANTITY);
		} else {
			Hide();
		}

		GlobalPosition = GetGlobalMousePosition();
		NullItemCheck();
	}

	public void GrabDragData(Item _item){
		item = _item;
		textureRect.Texture = item.ITEM_TEXTURE;
		hasItem = true;
	}
	public Item DropDragData(){
		hasItem = false;
		return item;
	}

	public void NullItemCheck(){
		if (item != null && item.ITEM_QUANTITY <= 0){
			DropDragData();
		}
	}

}
// item landed inventory, index
