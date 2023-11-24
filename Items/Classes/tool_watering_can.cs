using Godot;
using System;

public partial class tool_watering_can : Tool
{
    [Export]
    float waterLevel = 1;
    GameManager gameManager;
    TileMap tileMap;
    [Export]
    public Texture2D textureFull;
    [Export]
    public Texture2D textureEmpty;



    public tool_watering_can(){
        var tree = (SceneTree)Engine.GetMainLoop();
        gameManager = tree.Root.GetNode<GameManager>("GameManager");
        tileMap = gameManager.tileMap;
        gameManager.HasTileMap += GetTileMap;
    }
    void GetTileMap(TileMap _tileMap){
        tileMap = _tileMap;
    }

    public void Water(Vector2 globalPosition){
        var tilePos = tileMap.LocalToMap(globalPosition);
        int flowerLayer = 4;
        var hasPlant = tileMap.GetCellAlternativeTile(flowerLayer,tilePos);
        if (hasPlant != -1){


            if (waterLevel > 0){
                gameManager.EmitSignal("PlantWatered", tilePos);
                waterLevel -= 0.2f;

                if (waterLevel <= 0){
                    ITEM_TEXTURE = textureEmpty;
                    waterLevel = 0;
                }
                else {
                    ITEM_TEXTURE = textureFull;
                }
            }

        }

        GD.Print(waterLevel);
    }
}
//hasplantistä id ja korditaaleilla ehkä pystyy kutsua methodin


//joko signal tai interaction area