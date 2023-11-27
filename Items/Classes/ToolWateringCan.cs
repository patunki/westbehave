using Godot;
using System;

public partial class ToolWateringCan : Tool
{
    [Export]
    public float waterLevel = 1;
    [Export]
    public Texture2D textureFull;
    [Export]
    public Texture2D textureEmpty;


}
//hasplantistä id ja korditaaleilla ehkä pystyy kutsua methodin


//joko signal tai interaction area