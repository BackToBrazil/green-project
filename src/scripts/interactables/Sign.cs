using Godot;
using Godot.Collections;
using System;
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
        Interact();
    }

    public void Interact()
    {
        EventsManager.Instance.EmitSignal(EventsManager.SignalName.InteractionStarted, this);
    }
}
