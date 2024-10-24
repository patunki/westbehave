using Godot;
using System;

public partial class SeedScene : ItemScene
{
    public Seed seed;
    public Entity entity;
    GameManager gameManager;
    TileMapLayer tileMap;
    TileMapLayer plantLayer;
    TextureRect itemTexture;

    int groundLayer = 1;
    int flowerLayer = 4;
    int sceneId = 1;

    public override void _Ready()
    {
        itemTexture = GetNode<TextureRect>("Item");
        gameManager = GetNode<GameManager>("/root/GameManager");
        tileMap = gameManager.tileMap;
        gameManager.HasTileMap += GetTileMap;

    }

    void GetTileMap(TileMapLayer map){
        tileMap = map;
    }

    public override void MyItem(Item item, Entity _entity){
        if (itemTexture == null){
            itemTexture = GetNode<TextureRect>("Item");
        }
        seed = (Seed)item;
        entity = _entity;
        itemTexture.Texture = seed.ITEM_TEXTURE;
        
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("interact")){
        plantLayer = tileMap.GetNode<TileMapLayer>("Plants");
        var tilePos = tileMap.LocalToMap(GlobalPosition);
        var atlasCoord = new Vector2I(0,0);
        TileData tileData = tileMap.GetCellTileData(tilePos);
        var hasPlant = plantLayer.GetCellAlternativeTile(tilePos);
        GD.Print(tileData.GetCustomData("CanPlant"));
        if (tileData != null && hasPlant == -1){
            bool canPlant = (bool)tileData.GetCustomData("CanPlant");
                if (canPlant){
                    
                    plantLayer.SetCell(tilePos,sceneId,atlasCoord,1);
                    seed.DecQuant();
                }
            

        }
        else{
            GD.Print("Can't plant here or there is no tiledata ");
        }
        }
    }
}
