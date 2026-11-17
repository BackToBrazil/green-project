using Godot;
using Godot.Collections;
using System;
using System.Threading.Tasks;
namespace GreenProject.Interactable;

public partial class Sign : Node2D, Interactable
{
    [Export]
    public Array<string> Lines { get; private set; }

    private Area2D _area;

    public override void _Ready()
    {
        _area = GetNode<Area2D>("%Area2D");
        _area.AreaEntered += OnAreaEntered;
    }

    private void OnAreaEntered(Area2D area)
    {
        _ = Interact();
    }

    public async Task Interact()
    {
        bool hasContinuation = true;
        for (int i = 1; i < Lines.Count; i++)
        {
            if(i+1>=Lines.Count)
                hasContinuation = false;
            EventsManager.Instance.EmitSignal(EventsManager.SignalName.DialogStarted, Lines[i], Lines[0], hasContinuation);
            await ToSignal(EventsManager.Instance, EventsManager.SignalName.DialogFinished);
        }
    }
}
