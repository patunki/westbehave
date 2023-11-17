using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public partial class InteractionManager : Node2D
{
    private player player;
    private Label label;
    const string baseText = "[E] TO ";
    private List<InteractionArea> activeAreas = new List<InteractionArea>();
    bool canInteract = true;
    public override void _Ready()
    {
        player = (player)GetTree().GetFirstNodeInGroup("Player");
        label = GetNode<Label>("Label");

    }

    public void RegisterArea(InteractionArea area){
        activeAreas.Add(area);
    }
    
    public void UnregisterArea(InteractionArea area){
        var index = activeAreas.IndexOf(area);
        if (index != -1){
            activeAreas.RemoveAt(index);

        }
    }

    public override void _Process(double delta){
        if (activeAreas.Count > 0 && canInteract){
            label.Text = baseText + activeAreas[0].actionName;
            label.GlobalPosition = activeAreas[0].GlobalPosition;
            label.Show();
        }
        else {label.Hide();
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("interact") && canInteract){
            if (activeAreas.Count > 0){
                canInteract = false;
                label.Hide();

                activeAreas[0].callable.Call();
                canInteract = true;

            }
        }
    }





}
