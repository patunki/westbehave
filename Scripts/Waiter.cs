using Godot;
using System;
using System.Threading.Tasks;

public partial class Waiter : Node
{
    public async Task DelayMethod(float time){
        GD.Print("start");
        await Task.Delay(TimeSpan.FromSeconds(time));
        GD.Print("end");
    }
}
