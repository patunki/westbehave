using Godot;
using System;

public partial class tool_watering_can : ItemClass_Tool
{
    float waterLevel;
    GameManager gameManager;
    TileMap tileMap;

    public tool_watering_can(){
        var tree = (SceneTree)Engine.GetMainLoop();
        gameManager = tree.Root.GetNode<GameManager>("GameManager");
        gameManager.HasTileMap += GetTileMap;
        waterLevel = 1;
    }
    void GetTileMap(TileMap _tileMap){
        tileMap = _tileMap;
    }

    public void UseItem(Vector2 globalPosition){
        var tilePos = tileMap.LocalToMap(globalPosition);
        waterLevel -= 0.2f;
        if (waterLevel < 0){
            waterLevel = 0;
        }
        TileData tileData = tileMap.GetCellTileData(4,tilePos,true);
        GD.Print(tileData);

        GD.Print(waterLevel);
    }
}
