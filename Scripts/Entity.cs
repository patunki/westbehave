using Godot;
using System;

public enum EntityState {
	Alive,
	Dead,
	Uncon,
	NotExist
}
public enum EntityType {
    Unknown,
    Enemy,
    Animal,
    Friendly,
    Player,
}
public enum EntityGuild {
    Unknown,
    Zombies,
    Bandits,
    Survivor
}
public enum EntityBehavior {
    Undefined,
    Hostile,
    Friendly,
    Passive,
    Fearful

}
public enum EntityActivity {
    Idle,
    Fishing
}

public partial class Entity : CharacterBody2D
{
    [Export]
    public string EntityName;
    [Export]
    public EntityState entityState = EntityState.Alive;
    [Export]
    public EntityType entityType;
    [Export]
    public EntityGuild entityGuild;
    [Export]
    public EntityBehavior entityBehavior;
    [Export]
    public EntityActivity entityActivity;
    [Export]
    public Inventory inventory;
    public bool canMove = true;

    public void Die(){
        entityState = EntityState.Dead;
        canMove = false;
    }
    public virtual void EnterFSMState(string stateName){

    }


}
