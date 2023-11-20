using Godot;
using System;

public partial class GameLevel : Node2D
{
    TileMap tileMap;

    public override void _Ready(){

        tileMap = GetNode<TileMap>("TileMap");
        
    }

    public override void _Input(InputEvent @event)
    {   
        if (Input.IsActionJustPressed("click")){
            var tilePos = tileMap.LocalToMap(GetGlobalMousePosition());
            var atlasCoord = new Vector2I(0,0);
            TileData tileData = tileMap.GetCellTileData(3, tilePos);
            bool canPlant = (bool)tileData.GetCustomData("CanPlant");
            if (tileData != null && canPlant){
                
                tileMap.SetCell(0,tilePos,1,atlasCoord,1);
            }
            GD.PrintT(tileData);
            
        }
    }


}
