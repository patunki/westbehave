using Godot;
using System;
using System.Collections;

public partial class Food : Item
{
    [Export]
    public int HEALTH_RESTORED = 1;

    public void Eat(){
        DecQuant();
    }
    
}
