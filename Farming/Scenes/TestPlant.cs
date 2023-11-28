using Godot;
using System;

public enum PlantState {
    seed,
    sapling,
    grown,
    ripe,
    dead
}

public partial class TestPlant : Node2D
{
    [Export]
    Item yeld;
    Timer growTimer;
    Timer dryTimer;
    Timer fruitTimer;
    Sprite2D sprite;
    public PlantState state;
    InteractionArea interactionArea;
    Player nearestPlayer;
    Inventory playerInventory;
    TileMap tileMap;
    GameManager gameManager;


    public override void _Ready(){
        growTimer = GetNode<Timer>("GrowTimer");
        dryTimer = GetNode<Timer>("DryTimer");
        fruitTimer = GetNode<Timer>("FruitTimer");
        sprite = GetNode<Sprite2D>("Sprite2D");
        gameManager = GetNode<GameManager>("/root/GameManager");
        interactionArea = GetNode<InteractionArea>("InteractionArea");
        interactionArea.callable = Callable.From(() => interactionArea.Interact(this, "OnHarvest"));
        interactionArea.actionName = "HARVEST";
        state = PlantState.seed;
        interactionArea.PlayerEntered += GetPlayer;
        playerInventory = GD.Load<Inventory>("res://Player/PlayerInventory.tres");
        tileMap = GetParent<TileMap>();
        interactionArea.Monitoring = false;
        gameManager.PlantWatered += WaterPlant;
        growTimer.Timeout += GrowPlant;
        fruitTimer.Timeout += GrowFruit;
        dryTimer.Timeout += KillPlant;
        
    }

    void WaterPlant(Vector2I pos){
        var tilePos = tileMap.LocalToMap(Position);
        if (pos == tilePos){
            dryTimer.Start();
            GD.Print(dryTimer.TimeLeft);
        }
    }

    void GrowFruit(){
        state = PlantState.ripe;
        interactionArea.Monitoring = true;
        sprite.Frame = 3;
    }

    void KillPlant(){
        state = PlantState.dead;
        fruitTimer.QueueFree();
        dryTimer.QueueFree();
        interactionArea.actionName = "COLLECT";
        interactionArea.Monitoring = true;
        sprite.Frame = 5;

    }

    void GrowPlant(){
        sprite.Frame++;
        state = (PlantState)(((int)state + 1) % Enum.GetValues(typeof(PlantState)).Length); //SWITCHES TO THE NEXT STATE!
        if (state == PlantState.ripe){
            growTimer.QueueFree();
            interactionArea.Monitoring = true;
        }
        
    }
    //Add && has plant
    private void OnHarvest(){
        
        int plantLayer = 4;

        if (state == PlantState.ripe){
            playerInventory.AddItem(yeld, 2);
            state = PlantState.grown;
            sprite.Frame = 4;
            interactionArea.Monitoring = false;
            fruitTimer.Start();

        }
        if (state == PlantState.dead){
            Vector2I tilePos = tileMap.LocalToMap(GlobalPosition);
            tileMap.EraseCell(plantLayer,tilePos);
        }
            
            //tileMap.EraseCell(0,tilePos);
            //Vector2I tilePos = tileMap.LocalToMap(GlobalPosition);
            //QueueFree();

    }


    private void GetPlayer(Node2D player){ //USELESS ATM
        nearestPlayer = (Player)player;

    }

}
