using Godot;
using System;

public partial class InitialTileMap : TileMap
{
    GameManager gameManager;

    public override void _Ready()
    {
        gameManager = GetNode<GameManager>("/root/GameManager");
        gameManager.SetTileMap(this);
    }
}
