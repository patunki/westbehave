using Godot;
using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;


public partial class Collectable : Area2D
{

	[Export]
	private ItemClass itemResource;
	public Sprite2D itemSprite;

	public override void _Ready()
	{
		itemSprite = GetNode<Sprite2D>("Sprite2D");
		itemSprite.Texture = itemResource.ITEM_TEXTURE;
	}

	public void Collect(){
		QueueFree();
	}

	public ItemClass Give(){

		return itemResource.Copy();
	}



}
