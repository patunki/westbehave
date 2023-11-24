using Godot;
using System;

public partial class seed_eggplant_seed : Item_Seed
{
    GameManager gameManager;
    TileMap tileMap;

    public seed_eggplant_seed(){
        var tree = (SceneTree)Engine.GetMainLoop();
        gameManager = tree.Root.GetNode<GameManager>("GameManager");
        tileMap = gameManager.tileMap;
        gameManager.HasTileMap += GetTileMap;
        
    }

    void GetTileMap(TileMap map){
        tileMap = map;
    }

    public void Plant(Vector2 globalPosition){

        int groundLayer = 1;
        int flowerLayer = 4;
        int sceneId = 1;

        var tilePos = tileMap.LocalToMap(globalPosition);
        var atlasCoord = new Vector2I(0,0);
        TileData tileData = tileMap.GetCellTileData(groundLayer, tilePos);
        var hasPlant = tileMap.GetCellAlternativeTile(flowerLayer,tilePos);
        if (tileData != null && hasPlant == -1){
            bool canPlant = (bool)tileData.GetCustomData("CanPlant");
                if (canPlant){
                    
                    tileMap.SetCell(flowerLayer,tilePos,sceneId,atlasCoord,1);
                    DecQuant();
                }
            

        }
        else{
            GD.Print("Can't plant here or there is no tiledata ");
        }

    }


}