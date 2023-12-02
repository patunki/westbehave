using Godot;
using System;
using System.Drawing;

public partial class FishingRod : ItemScene
{
    Entity entity;
    Sprite2D koho;
    Sprite2D rod;
    Godot.Color lineColor = new Godot.Color(1, 0, 0, 1);
    AnimationPlayer animationPlayer;
    bool isFishing = false;
    bool canFish = true;
    FishingSystem fishingSystem = new FishingSystem();

    int rodEfficensy;
    float rodQuality;
    ToolFishingRod fishingRod;

    public override void _Ready()
    {
        koho = GetNode<Sprite2D>("Koho");
        rod = GetNode<Sprite2D>("Rod");
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public override void MyItem(Item item, Entity _entity){
        fishingRod = (ToolFishingRod)item;
        entity = _entity;
        rodEfficensy = fishingRod.Efficensy;
        rodQuality = fishingRod.Quality;
    }

    public override void _PhysicsProcess(double delta)
    {
        QueueRedraw();
        if (isFishing){
            
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("attack") && !isFishing){
            entity.EnterFSMState("Fishing");
            isFishing = true;
            animationPlayer.Play("Throw");
            FishingLoop();
        }

    }

    public override void _Draw()
    {
        DrawLine(koho.Position, rod.Position,lineColor,1);
    }

     void FishingLoop(){
        Item caught = fishingSystem.TryCatchFish(rodEfficensy,rodQuality);
        if (caught != null){
            animationPlayer.Play("CatchFish");
            entity.inventory.AddItem(caught,1);
        }
        else {
            animationPlayer.Play("ReelIn");
        }
        isFishing = false;
        entity.EnterFSMState("Idle");
    }

}

public partial class FishingSystem: Node
{
    RandomNumberGenerator random = new RandomNumberGenerator();
    int randomed;
    public Item TryCatchFish(int efficensy, float quality){
        randomed = random.RandiRange(1,100);
        Item item = GD.Load("res://Items/Repo/FoodRawFish.tres") as Item;
        if (efficensy + randomed > 100){
            return item;
        }

        return null;
    }

}
