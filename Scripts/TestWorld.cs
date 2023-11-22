using Godot;
using System;

public partial class TestWorld : Node2D
{   

    public void _on_area_2d_body_entered(Node2D body){
        GetTree().ChangeSceneToFile("res://Levels/game_level.tscn");
    }
}
