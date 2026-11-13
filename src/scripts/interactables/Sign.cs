using Godot;
using Godot.Collections;
using System;
namespace GreenProject.Interactable;

public partial class Sign : Node2D, Interactable
{
    [Export]
    private Area2D _area;

    [Export]
    public string Text {  get; private set; }
    [Export]
    public Array<string> Lines { get; private set; }

    public override void _Ready()
    {
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
