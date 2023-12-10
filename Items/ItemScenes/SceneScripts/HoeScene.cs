using Godot;
using System;

public partial class HoeScene : ItemScene
{
	Tool tool;
	Entity entity;
	TextureRect textureRect;
	AnimationPlayer animationPlayer;
	GameManager gameManager;
    TileMap tileMap;
    Godot.Collections.Array<Godot.Vector2I> dirtTiles = new Godot.Collections.Array<Godot.Vector2I>();

    public override void _Ready(){

        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		gameManager = GetNode<GameManager>("/root/GameManager");
		gameManager.HasTileMap += GetTileMap;
		tileMap = gameManager.tileMap;
    }
    public override void MyItem(Item _item, Entity _entity){
		tool = (Tool)_item;
		entity = _entity;
		textureRect = GetNode<TextureRect>("ItemTex");
		textureRect.Texture = tool.ITEM_TEXTURE;
	}
	void GetTileMap(TileMap map){
        tileMap = map;
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("click")){
		int groundLayer = 1;
        //int sceneId = 2;

        Vector2I tilePos = tileMap.LocalToMap(GlobalPosition);
        //var atlasCoord = new Vector2I(3,2);
        TileData tileData = tileMap.GetCellTileData(groundLayer, tilePos);
        bool canSet = (bool)tileData.GetCustomData("CanPlaceDirt");
        
        if (canSet){
            
            dirtTiles.Add(tilePos);
            tileMap.SetCellsTerrainConnect(groundLayer,dirtTiles,0,0,false);
            //tileMap.SetCell(groundLayer,tilePos,sceneId,atlasCoord);
                
        }
		}
    }


}
