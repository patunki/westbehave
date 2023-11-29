using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node2D
{
    [Signal]
    public delegate void ItemLiftedEventHandler(int index);
    [Signal]
    public delegate void ItemLandedEventHandler(); 
    [Signal]
    public delegate void SlotClickedEventHandler(Item item);
    [Signal]
    public delegate void HasTileMapEventHandler(TileMap tileMap);
    [Signal]
    public delegate void ExternalInventoryRecieveEventHandler(ExternalInventory inv, Item item, int quant);
    [Signal]
    public delegate void PlantWateredEventHandler(Vector2I tilepos);
    public TileMap tileMap;
    public SlotData slotData;

    public override void _Ready()
    {
        slotData = GetTree().GetFirstNodeInGroup("SlotData") as SlotData;
        GD.Print("God Slot data as ", slotData);
    }

    public void SetTileMap(TileMap map){
        tileMap = map;
        EmitSignal(SignalName.HasTileMap,tileMap);
    }





    /*public void RegisterTileMap(TileMap map){
        tileMaps.Add(map);  
    }

    public void UnregisterArea(TileMap map){
        var index = tileMaps.IndexOf(map);
        if (index != -1){
            tileMaps.RemoveAt(index);

        }
    }  */
     //private List<TileMap> tileMaps = new List<TileMap>();

}
