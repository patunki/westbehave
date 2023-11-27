using Godot;
using System;
using System.Linq;

public partial class ToolHoe : Tool
{
    GameManager gameManager;
    TileMap tileMap;

    public ToolHoe(){
        var tree = (SceneTree)Engine.GetMainLoop();
        gameManager = tree.Root.GetNode<GameManager>("GameManager");
        tileMap = gameManager.tileMap;
        gameManager.HasTileMap += GetTileMap;
        
    }

    void GetTileMap(TileMap map){
        tileMap = map;
    }

    public void OnInteract(Vector2 globalPosition){
        int groundLayer = 1;
        int sceneId = 2;

        var tilePos = tileMap.LocalToMap(globalPosition);
        var atlasCoord = new Vector2I(3,2);
        TileData tileData = tileMap.GetCellTileData(groundLayer, tilePos);
        
        if (tileData != null){

            tileMap.SetCell(groundLayer,tilePos,sceneId,atlasCoord);
                
        }

    }

}
