using Godot;
using System;
namespace GreenProject.UI;

public partial class UserInterface : Control
{
    [Export]
    public PanelContainer DialogBox {  get; private set; }

    public override void _Ready()
    {
        EventsManager.Instance.InteractionStarted += OnInteraction;
    }

    private async void OnInteraction(Node2D origin)
    {
        DialogBox.Call("pop_open");
        DialogBox.Call("say", origin.Get("Text"));
        await ToSignal(DialogBox, "advanced");
        EventsManager.Instance.EmitSignal(EventsManager.SignalName.InteractionFinished, origin);
        DialogBox.Call("pop_close");
    }
}
