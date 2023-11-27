using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Nodes;
using System.Text;


public partial class ItemDatabase : Node
{   

    public Godot.Collections.Array<Item> itemDatabase = new Godot.Collections.Array<Item>();
    public Godot.Collections.Dictionary<int, Item> dictionary = new Godot.Collections.Dictionary<int, Item>();
    public Dictionary<int, List<string>> Recipes = new Dictionary<int, List<string>>();
    public override void _Ready()
    {   
        string folderPath = "Items/Repo/";
        LoadItems(folderPath);
        //CRAFTING RECEPIES
        //Recipes[1] = new List<string>{"Apple","Eggplant"};
        //Recipes[2] = new List<string>{"Apple","Eggplant seed"};
        Recipes[9] = new List<string>{"Planks", "Iron Ore"};


        //Recipes.TryGetValue(1, out var ingerdients);
        //foreach (string ingredient in ingerdients){
        //    GD.Print(ingredient);
        //}
        
    }


    public void LoadItems(string folderPath){
        string[] files = Directory.GetFiles(folderPath, "*.tres");
        foreach (string file in files){
            itemDatabase.Add(GD.Load<Item>(file));    
        }
        
        WriteToFile();
    }

    public Item GetItemById(int id){
        foreach (Item item in itemDatabase){
            if (item != null && item.ITEM_ID == id){
                GD.Print("Itemi löytyy databasessa");
                return item;
            }
        }
        GD.Print("itemiä ei databasessa");
        return null;
    }

    public void WriteToFile(){

        foreach (Item item in itemDatabase){
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
            //itemObject["Primary Method"] = item.Value.Action;
            itemObject["Dev nodes"] = item.Value.DevNotes;
            array.Add(itemObject);
        }
        
        var jsonString = Json.Stringify(array.ToJsonString(), "", true, true);
        var error = json.Parse(jsonString);
        string filePath = "Database/ItemDB.json";
        using var file = Godot.FileAccess.Open(filePath, Godot.FileAccess.ModeFlags.Write);

        ConvertJSONArrayToCSV(array);

        file.StoreLine(json.Data.ToString());
        GD.Print("Database updated");
        
    }

    //This part is from chatGgpt:
    public void ConvertJSONArrayToCSV(JsonArray jsonArray)
    {

        string csvFilePath = "Database/CSV/ItemDB.csv";

        if (jsonArray == null)
        {
            GD.Print("JSON array is null");
            return;
        }
        if (File.Exists(csvFilePath))
        {
            File.Delete(csvFilePath);
            GD.Print("Deleted: " + csvFilePath );
        }

        // Create a StringBuilder to build the CSV content
        StringBuilder csvContent = new StringBuilder();

        // Write header (based on the keys of the first item in the array)
        if (jsonArray.Count > 0 && jsonArray[0] is JsonObject firstItem)
        {
            List<string> keys = GetJsonObjectKeys(firstItem);
            csvContent.AppendLine(string.Join(",", keys));
        }

        // Write data rows
        foreach (JsonObject itemObject in jsonArray)
        {
            List<string> values = new List<string>();

            foreach (string key in GetJsonObjectKeys(itemObject))
            {
                values.Add(itemObject[key].ToString());
            }

            csvContent.AppendLine(string.Join(",", values));
        }

        // Save the CSV content to a file
        File.WriteAllText(csvFilePath, csvContent.ToString());

        GD.Print("New CSV file saved to: " + csvFilePath);
    }

    private List<string> GetJsonObjectKeys(JsonObject jsonObject)
    {
        List<string> keys = new List<string>();

        foreach (var keyValuePair in jsonObject)
        {
            keys.Add(keyValuePair.Key);
        }

        return keys;
    }

    

}
