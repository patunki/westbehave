using Godot;
using System;

public partial class HealthComponent : Node2D
{
    [Export]
    public int health = 10;
    [Export]
    Label label;
    [Signal]
    public delegate void HealthDepletedEventHandler();
    bool hasLable = true;

    public override void _Ready()
    {
        try {
            label.Text = "Health: " + health.ToString();
        }
        catch{
            hasLable = false;
        }

    }
    public void Damage(Attack attack){
        
        health -= attack.Damage;
        if (health <= 0 ){
            EmitSignal(SignalName.HealthDepleted);
            
        }
        UpdateLabel();
    }

    public void Starve(int damage){
        health -= damage;
        UpdateLabel();
        
    }

    void UpdateLabel(){
        if (health <= 0){
            health = 0;
            EmitSignal(SignalName.HealthDepleted);
        }
        if (hasLable){
            label.Text = "Health: " + health.ToString();
        }
    }

}
