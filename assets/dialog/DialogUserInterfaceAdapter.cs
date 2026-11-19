using Godot;
using Godot.Collections;
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

    public override async Task<int> ShowChoices(Array<string> choices)
    {
        foreach (string choice in choices)
        {
            _ui.ChoiceGrid.Call("set_items", choices);
        }
        
        _ui.ChoiceMenu.CallDeferred("pop_open");
        _ui.DialogBox.CallDeferred("pop_close");

        var result = await ToSignal(_ui.ChoiceGrid, "chosen");

        _ui.ChoiceMenu.CallDeferred("pop_close");
        _ui.DialogBox.CallDeferred("pop_open");

        return (int)result[0];
    }
}
