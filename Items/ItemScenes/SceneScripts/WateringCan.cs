using Godot;
using System;

public partial class WateringCan : Node2D
{

    Entity entity;
    ToolWateringCan can;
    GameManager gameManager;
    TileMap tileMap;
    AnimationPlayer animationPlayer;

    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        gameManager = GetNode<GameManager>("/root/GameManager");
        tileMap = gameManager.tileMap;
        gameManager.HasTileMap += GetTileMap;
        if (can != null && can.waterLevel <= 0){
            animationPlayer.Play("Empty");
        }
        
        
    }

    public void MyItem(Item item, Entity _entity){
        can = (ToolWateringCan)item;
        entity = _entity;

    }

    void GetTileMap(TileMap _tileMap){
        tileMap = _tileMap;
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("interact")){
        var tilePos = tileMap.LocalToMap(GlobalPosition);
        int flowerLayer = 4;
        var hasPlant = tileMap.GetCellAlternativeTile(flowerLayer,tilePos);
        if (hasPlant != -1){


            if (can.waterLevel > 0){
                animationPlayer.Play("Water");
                gameManager.EmitSignal("PlantWatered", tilePos);
                can.waterLevel -= 0.2f;
            }
            if (can.waterLevel <= 0){
                can.ITEM_TEXTURE = can.textureEmpty;
                animationPlayer.Play("Empty");
                entity.inventory.EmitChange();
                can.waterLevel = 0;
                }
            else {
                can.ITEM_TEXTURE = can.textureFull;
            }
            
        }
        GD.Print(can.waterLevel);
        }
    }
}