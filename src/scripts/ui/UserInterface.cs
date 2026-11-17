using Godot;
using Godot.Collections;
using System;
namespace GreenProject.UI;

public partial class UserInterface : Control
{
    [Export]
    public PanelContainer DialogBox {  get; private set; }

    private bool _isDialogOpen = false;
    public override void _Ready()
    {
        EventsManager.Instance.DialogStarted += OnDialogStart;
    }

    private async void OnDialogStart(string line, string name, bool hasContinuation)
    {
        if (!_isDialogOpen)
        {
            DialogBox.Call("pop_open");
            _isDialogOpen = true;
        }

        DialogBox.Call("say", line, name);
        await ToSignal(DialogBox, "advanced");
        EventsManager.Instance.EmitSignal(EventsManager.SignalName.DialogFinished);

        if (!hasContinuation)
        {
            DialogBox.Call("pop_close");
            _isDialogOpen = false;
        }
    }
}
