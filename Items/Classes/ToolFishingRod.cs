using Godot;
using System;

public partial class ToolFishingRod : Tool
{
    [Export(PropertyHint.Range, "0,100")]
    public int Efficensy;
    [Export(PropertyHint.Range, "0,1")]
    public float Quality;
}
