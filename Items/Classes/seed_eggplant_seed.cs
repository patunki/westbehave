using Godot;
using System;

public partial class seed_eggplant_seed : ItemClass_Seed
{
    GameManager gameManager;
    TileMap tileMap;

    public seed_eggplant_seed(){
        var tree = (SceneTree)Engine.GetMainLoop();
        gameManager = tree.Root.GetNode<GameManager>("GameManager");
        gameManager.HasTileMap += GetTileMap;
        
    }

    void GetTileMap(TileMap map){
        tileMap = map;
        GD.Print(tileMap);
    }

    public void Plant(Vector2 globalPosition){

        int groundLayer = 1;
        int flowerLayer = 4;
        int sceneId = 1;

        var tilePos = tileMap.LocalToMap(globalPosition);
        var atlasCoord = new Vector2I(0,0);
        TileData tileData = tileMap.GetCellTileData(groundLayer, tilePos);
        if (tileData != null){
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
