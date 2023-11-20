using Godot;
using System;

public enum PlantState {
    seed,
    sapling,
    ripe
}

public partial class TestPlant : Node2D
{
    [Export]
    ItemClass yeld;
    Timer timer;
    Sprite2D sprite;
    public PlantState state;
    InteractionArea interactionArea;
    player nearestPlayer;


    public override void _Ready(){
        timer = GetNode<Timer>("Timer");
        sprite = GetNode<Sprite2D>("Sprite2D");
        interactionArea = GetNode<InteractionArea>("InteractionArea");
        interactionArea.callable = Callable.From(() => interactionArea.Interact(this, "OnCollect"));
        state = PlantState.seed;
        timer.Timeout += GrowPlant;
        interactionArea.PlayerEntered += GetPlayer;
    }

    void GrowPlant(){
        sprite.Frame++;
        state = (PlantState)(((int)state + 1) % Enum.GetValues(typeof(PlantState)).Length); //SWITCHES TO THE NEXT STATE!
        if (state == PlantState.ripe){
            timer.QueueFree();
        }
        
    }

    private void OnCollect(){
        if (state == PlantState.ripe){ //Add && has plant
            QueueFree();
        } 
        else {
            GD.Print("ei oo vielä valmis");
        }
    }

    private void GetPlayer(Node2D player){
        GD.Print(player.Name);
    }

}
