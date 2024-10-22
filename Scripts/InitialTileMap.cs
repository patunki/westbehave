using Godot;
using System;

public partial class InitialTileMap : TileMapLayer
{
    GameManager gameManager;

    public override void _Ready()
    {
        gameManager = GetNode<GameManager>("/root/GameManager");
        gameManager.SetTileMap(this);
    }
}
