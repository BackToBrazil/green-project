using Godot;
using Godot.Collections;
using System;
using System.Threading.Tasks;
namespace GreenProject.Dialog;

[GlobalClass]
public partial class BranchingDialog : IDialog
{
    [Export(PropertyHint.File, "*.json")]
    public string Dialogs { get; private set; }

    private string _name;
    private Array<string> _lines;
    private Array<string> _choices;
    private Array<Array<string>> _branches;
    int _branch = 0;

    public override void _Ready()
    {
        Dictionary<string, Variant> data_received = (Dictionary<string, Variant>)ResourceLoader.Load<Json>(Dialogs).Data;

        if (data_received.ContainsKey("lines") && data_received.ContainsKey("name") && data_received.ContainsKey("branches") && data_received.ContainsKey("choices"))
        {
            _lines = (Array<string>)data_received["lines"];
            _name = (string)data_received["name"];
            _branches = (Array<Array<string>>)data_received["branches"];
            _choices = (Array<string>)data_received["choices"];
        }
        else
        {
            GD.PrintErr(string.Format("Could not read {0}'s Dialog.json, check for formatting error.", Name));
            return;
        }

        GetNode<Area2D>("%Area2D").AreaEntered += (obj) => { EventsManager.Instance.EmitSignal(EventsManager.SignalName.DialogStarted, this); };
    }

    public override async Task SayLines(IDialogService service)
    {
        Array<string> lines = _lines;

        if (lines == null || lines.Count <= 0)
        {
            GD.PrintErr(string.Format("{0}'s lines file '{1}' is null or empty.", _name, lines));
            return;
        }

        for (int i = 0; i < lines.Count; i++)
        {
            await service.SayLine(lines[i], _name);
        }

        _branch = await service.ShowChoices(_choices);

        lines = _branches[_branch];

        for (int i = 0; i < lines.Count; i++)
        {
            await service.SayLine(lines[i], _name);
        }

        service.DialogFinished();
    }
}
