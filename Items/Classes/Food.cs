using Godot;
using System;
using System.Collections;

public partial class Food : Item
{
    [Export]
    public int FoodValue;
    [Export]
    public int WaterValue;

    public int Eat(){
        DecQuant();
        return FoodValue;
    }
    public int Drink(){
        return WaterValue;
    }
    
}
