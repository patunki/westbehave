using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public partial class ItemDatabase : Node
{   

    public Godot.Collections.Array<ItemClass> itemDatabase = new Godot.Collections.Array<ItemClass>();

    public override void _Ready()
    {   
        string folderPath = "Items/Repo/"; //should be res://Items/Repo/. FIX THIS!
        LoadItems(folderPath);
        
        
    }

    public void LoadItems(string folderPath){
        string[] files = Directory.GetFiles(folderPath, "*.tres");
        foreach (string file in files){
            itemDatabase.Add(GD.Load<ItemClass>(file));    
        }
        
    }

    public ItemClass GetItem(int id){
        for (int i = 0; i < itemDatabase.Count; i++){
            if (itemDatabase[i] != null && itemDatabase[i].ITEM_ID == id){
                return itemDatabase[i];
            }
        }
        return null;
    }

}


// itemBase.Add(GD.Load(file));
// itemDatabase.Add(item.ITEM_ID, GD.Load<ItemClass>(file));
// Godot.Collections.Array itemDatabase;
// Dictionary<int, ItemClass> itemDatabase = new Dictionary<int, ItemClass>();
//ItemClass item = GD.Load<ItemClass>(file) as ItemClass;