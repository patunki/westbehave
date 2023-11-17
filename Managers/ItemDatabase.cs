using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Nodes;


public partial class ItemDatabase : Node
{   

    public Godot.Collections.Array<ItemClass> itemDatabase = new Godot.Collections.Array<ItemClass>();
    public Godot.Collections.Dictionary<int, ItemClass> dictionary = new Godot.Collections.Dictionary<int, ItemClass>();
    public override void _Ready()
    {   
        string folderPath = "Items/Repo/"; //should be "res://Items/Repo/"? FIX THIS?
        LoadItems(folderPath);
        
        
    }

    public void LoadItems(string folderPath){
        string[] files = Directory.GetFiles(folderPath, "*.tres");
        foreach (string file in files){
            itemDatabase.Add(GD.Load<ItemClass>(file));    
        }
        
        WriteToFile();
    }

    public ItemClass GetItem(int id){
        for (int i = 0; i < itemDatabase.Count; i++){
            if (itemDatabase[i] != null && itemDatabase[i].ITEM_ID == id){
                return itemDatabase[i];
            }
        }
        return null;
    }

    public void WriteToFile(){


        foreach (ItemClass item in itemDatabase){
            dictionary[item.ITEM_ID] = item;
        }
        Json json = new Json();
        JsonArray array = new JsonArray();

        foreach (var item in dictionary){
            JsonObject itemObject = new JsonObject();
            itemObject["ITEM_ID"] = item.Value.ITEM_ID;
            itemObject["ITEM_NAME"] = item.Value.ITEM_NAME;
            itemObject["RESOURCE"] = item.Value.CustomResourcePath;
            itemObject["IS_STACKABLE"] = item.Value.IS_STACKABLE;
            itemObject["HOVER_TEXT"] = item.Value.HOVER_TEXT;
            itemObject["ITEM_QUANTITY"] = item.Value.ITEM_QUANTITY;
            itemObject["MAX_SACK"] = item.Value.MAX_STACK;
            itemObject["ITEM_TYPE"] = item.Value.ITEM_TYPE.ToString();
            array.Add(itemObject);
        }
        
        var jsonString = Json.Stringify(array.ToJsonString(), "", true, true);
        var error = json.Parse(jsonString);
        string filePath = "Database/ItemDB.json";
        using var file = Godot.FileAccess.Open(filePath, Godot.FileAccess.ModeFlags.Write);

        file.StoreLine(json.Data.ToString());
        
    }

}
