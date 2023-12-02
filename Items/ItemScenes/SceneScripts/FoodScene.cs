using Godot;
using System;

public partial class FoodScene : ItemScene
{
    TextureRect foodTexture;
    Food food;
    Entity entity;
    HungerComponent hungerComponent;

    public override void MyItem(Item item, Entity _entity){
        food = (Food)item;
        entity = _entity;
        hungerComponent = entity.GetNode<HungerComponent>("HungerComponent");
        foodTexture = GetNode<TextureRect>("FoodTexture");
        foodTexture.Texture = food.ITEM_TEXTURE;
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed("interact")){
            Eat();
        }
    }

    public void Eat(){
        hungerComponent.WaterAdd(food.Drink());
        hungerComponent.FoodAdd(food.Eat());
    }

}
