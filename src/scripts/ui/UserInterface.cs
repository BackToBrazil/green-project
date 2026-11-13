using Godot;
using Godot.Collections;
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
        Array<string> lines = (Array<string>)origin.Get("Lines");
        if (lines.Count == 0)
            return;
        DialogBox.Call("pop_open");
        for (int i = 1; i < lines.Count; i++)
        {
            DialogBox.Call("say", lines[i], lines[0]);
            await ToSignal(DialogBox, "advanced");
        }
        EventsManager.Instance.EmitSignal(EventsManager.SignalName.InteractionFinished, origin);
        DialogBox.Call("pop_close");
    }
}
