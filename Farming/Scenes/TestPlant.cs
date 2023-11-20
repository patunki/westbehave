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
    InventoryClass playerInventory;
    TileMap tileMap;


    public override void _Ready(){
        timer = GetNode<Timer>("Timer");
        sprite = GetNode<Sprite2D>("Sprite2D");
        interactionArea = GetNode<InteractionArea>("InteractionArea");
        interactionArea.callable = Callable.From(() => interactionArea.Interact(this, "OnHarvest"));
        state = PlantState.seed;
        timer.Timeout += GrowPlant;
        interactionArea.PlayerEntered += GetPlayer;
        playerInventory = GD.Load<InventoryClass>("res://Player/PlayerInventory.tres");
        tileMap = GetParent<TileMap>();
        interactionArea.Monitoring = false;
        
    }

    void GrowPlant(){
        sprite.Frame++;
        state = (PlantState)(((int)state + 1) % Enum.GetValues(typeof(PlantState)).Length); //SWITCHES TO THE NEXT STATE!
        if (state == PlantState.ripe){
            timer.Stop();
            interactionArea.Monitoring = true;
        }
        
    }
    //Add && has plant
    private void OnHarvest(){
        
        if (state == PlantState.ripe){ 
            Vector2I tilePos = tileMap.LocalToMap(GlobalPosition);
            playerInventory.AddItem(yeld, 2);
            
            state = PlantState.seed;
            sprite.Frame = 0;
            timer.Start();
            interactionArea.Monitoring = false;
            //tileMap.EraseCell(0,tilePos);
            //QueueFree();

            
        } 
        else {
            GD.Print("ei oo vielä valmis");
        }
    }


    private void GetPlayer(Node2D player){ //USELESS ATM
        nearestPlayer = (player)player;

    }

}
