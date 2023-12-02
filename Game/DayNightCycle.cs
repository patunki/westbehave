using Godot;
using System;

public partial class DayNightCycle : CanvasModulate
{
    const int minutesPerDay = 1140;
    const int minutesPerHour = 60;
    const double inGameToTeal = (1 * Math.PI) / minutesPerDay;


    [Export]
    GradientTexture1D gradient;
    [Export]
    float inGameSpeed = 1;
    [Export]
    int initialHour;
    public double timeOfDay = 0.0f;

    public override void _Ready()
    {
        timeOfDay = inGameToTeal * initialHour * minutesPerHour;
    }

    public override void _Process(double delta)
    {
        timeOfDay += delta * inGameToTeal * inGameSpeed;
        float value = (float)(Math.Sin(timeOfDay - Math.PI / 2) + 1.0) / 2;
        Color = gradient.Gradient.Sample(value);
        //Recalculate();
    }

    void Recalculate(){
        //int totalMinutes = (int)(timeOfDay / inGameToTeal);
        //int currentDayMinutes = totalMinutes % minutesPerDay;
        //int currentHour = currentDayMinutes / minutesPerHour;
    }

}
