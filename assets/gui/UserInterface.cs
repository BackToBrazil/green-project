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
        EventsManager.Instance.LineSaid += OnSayLine;

        EventsManager.Instance.DialogFinished += () => { 
            DialogBox.Call("pop_close");
            EventsManager.Instance.EmitSignal(EventsManager.SignalName.DialogBoxClosed);
        };
    }

    private async void OnSayLine(string line, string name)
    {
        if (!GameState.Instance.IsDialogBoxOpen)
        {
            EventsManager.Instance.EmitSignal(EventsManager.SignalName.DialogBoxOpened);
            DialogBox.Call("pop_open");
        }

        DialogBox.Call("say", line, name);
        await ToSignal(DialogBox, "advanced");
        EventsManager.Instance.EmitSignal(EventsManager.SignalName.DialogAdvanced);
    }
}
