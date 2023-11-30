using Godot;
using System;

public partial class SlotData : Node2D
{
	TextureRect textureRect;
	Label label;
	public Item dragItem;
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
			if (dragItem.ITEM_QUANTITY > 1){
				label.Text = dragItem.ITEM_QUANTITY.ToString();
			}else{
				label.Text = "";
			}
			GD.Print(dragItem.ITEM_QUANTITY);
		} else {
			Hide();
		}

		GlobalPosition = GetGlobalMousePosition();
		NullItemCheck();
	}

	public void GrabSlotData(Item item){
		dragItem = item;
		textureRect.Texture = dragItem.ITEM_TEXTURE;
		hasItem = true;
	}
	public Item DropSlotData(){
		hasItem = false;
		return dragItem;
	}

	public void NullItemCheck(){
		if (dragItem != null && dragItem.ITEM_QUANTITY <= 0){
			DropSlotData();
		}
	}

}
// item landed inventory, index
