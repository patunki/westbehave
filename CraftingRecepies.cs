using Godot;
using System;
using System.Collections.Generic;

public partial class CraftingRecepies : Resource
{
    public Godot.Collections.Dictionary<int, Godot.Collections.Dictionary<string, int>> Recipes { get; set; } = new Godot.Collections.Dictionary<int, Godot.Collections.Dictionary<string, int>>{

        [9] = new Godot.Collections.Dictionary<string, int>{["Stick"] = 1, ["Iron"] = 2}, //Axe
        [13] = new Godot.Collections.Dictionary<string, int>{["Planks"] = 2}, //Campfire
        [14] = new Godot.Collections.Dictionary<string, int>{["Planks"] = 1} //Fishing rod

    };
 
}
