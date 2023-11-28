using Godot;
using System;

public partial class EnemyIdle : State
{
    Vector2 moveDirection;
    float wanderTime;
    Entity enemy;
    int moveSpeed;
    Entity player;



    void Randomize(){
        var random = new RandomNumberGenerator();
        moveDirection = new Vector2(random.RandfRange(-1,1),random.RandfRange(-1,1)).Normalized();
        wanderTime = random.RandfRange(1,5);
        moveSpeed = random.RandiRange(5,20);
        if (moveSpeed < 8){
            moveSpeed = 0;
        }
    }
    public override void Enter(Entity entity){
        enemy = entity;
        player = (Entity)GetTree().GetFirstNodeInGroup("Player");
        Randomize();
    }

    public override void Update(double delta){
        if (wanderTime > 0){
            wanderTime -= (float)delta;
        }
        else {
            Randomize();
        }

        Vector2 direction = player.GlobalPosition - enemy.GlobalPosition;
        float distance = direction.Length();

        if (distance < 50 && player.entityState == EntityState.Alive){
            EmitSignal(SignalName.Transitioned, this,"EnemyFollow");
        }
    }
    public override void PhysicsUpdate(double delta){
        if (enemy != null){
            enemy.Velocity = moveDirection * moveSpeed;
        }
    }
}
