using Godot;
using System;

public partial class HungerComponent : Node2D
{
    [Export]
    HealthComponent healthComponent;
    [Export(PropertyHint.Range, "0,100")]
    public int Food;
    [Export(PropertyHint.Range, "0,100")]
    public int Water;
    Timer hungerTimer;
    Timer thirstTimer;
    [Signal]
    public delegate void ThirtChangedEventHandler();
    [Signal]
    public delegate void HungerChangedEventHandler();
    [Export]
    Label hLabel;
    [Export]
    Label tLabel;
    bool hasLable = true;

    public override void _Ready()
    {
        hungerTimer = GetNode<Timer>("HungerTimer");
        thirstTimer = GetNode<Timer>("ThirstTimer");
        hungerTimer.Timeout += HungerAdd;
        thirstTimer.Timeout += ThirstAdd;
        try{
            hLabel.Text = "Food " + Food.ToString();
            tLabel.Text = "Water " + Water.ToString();
        }
        catch {
            hasLable = false;
        }
    }

    void HungerAdd(){
        Food -= 1;
        if (Food <= 0){
            Food = 0;
            healthComponent.Starve(1);
        }
        UpdateLabel();

    }
    void ThirstAdd(){
        Water -= 1;
        if (Water <= 0){
            Water = 0;
            healthComponent.Starve(1);
        }
        UpdateLabel();

    }
    public void FoodAdd(int food){
        Food += food;
        if (Food > 100){
            Food = 100;
        }
        UpdateLabel();
    }
    public void WaterAdd(int water){
        Water += water;
        if (Water > 100){
            Water = 100;
        }
        UpdateLabel();
    }
    void UpdateLabel(){
        if (hasLable){
            hLabel.Text = "Food: " + Food.ToString();
            tLabel.Text = "Water: " + Water.ToString();
        }
    }


}
