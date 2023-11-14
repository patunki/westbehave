using Godot;
using System;

public partial class ItemSprite : Sprite2D
{

	[Export]
	ItemClass ItemResource {set; get;}
	RichTextLabel label;

	public override void _Ready()
	{
		label = GetNode<RichTextLabel>("RichTextLabel");
	}

	public override void _PhysicsProcess(double delta)
	{
		Texture = ItemResource.ITEM_TEXTURE;
	}



}
