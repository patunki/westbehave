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
       
    }


}
