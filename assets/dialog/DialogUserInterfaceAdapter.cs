using Godot;
using GreenProject.UI;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace GreenProject.Dialog;

public partial class DialogUserInterfaceAdapter : IDialogService
{
    UserInterface _ui = null;

    public DialogUserInterfaceAdapter(UserInterface ui)
    {
        _ui = ui;
        EventsManager.Instance.DialogStarted += OnDialogStarted;
    }

    private void OnDialogStarted(IDialog dialog)
    {
        if (!GameState.Instance.IsDialogBoxOpen)
        {
            EventsManager.Instance.EmitSignal(EventsManager.SignalName.DialogBoxOpened);
            _ui.DialogBox.Call("pop_open");
            dialog.SayLines(this);
        }
    }

    public override void DialogFinished()
    {
        EventsManager.Instance.EmitSignal(EventsManager.SignalName.DialogBoxClosed);
        _ui.DialogBox.Call("pop_close");
    }

    public override async Task SayLine(string line, string name)
    {
        _ui.DialogBox.Call("say", line, name);
        await ToSignal(_ui.DialogBox, "advanced");
        return;
    }
}
