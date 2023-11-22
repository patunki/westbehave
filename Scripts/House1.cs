using Godot;
using System;

public partial class House1 : StaticBody2D
{


    public void _on_area_2d_body_entered(Node2D body){
        GetTree().ChangeSceneToFile("res://Scenes/TestWorld.tscn");
    }
}
