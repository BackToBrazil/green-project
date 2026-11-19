using Godot;
using Godot.Collections;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace GreenProject.Dialog;

[GlobalClass]
public partial class SimpleDialog : IDialog
{
    [Export(PropertyHint.File, "*.json")]
    public string Dialog { get; private set; }

    protected Array<string> _lines;
    protected string _name;

    public override void _Ready()
    {
        Dictionary<string, Variant> data_received = (Dictionary<string, Variant>)ResourceLoader.Load<Json>(Dialog).Data;

        if (data_received.ContainsKey("lines") && data_received.ContainsKey("name"))
        {
            _lines = (Array<string>)data_received["lines"];
            _name = (string)data_received["name"];
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
        if (_lines == null || _lines.Count <= 0)
        {
            GD.PrintErr(string.Format("{0}'s lines file '{1}' is null or empty.", _name, _lines));
            return;
        }

        for (int i = 0; i < _lines.Count; i++)
        {
            await service.SayLine(_lines[i], _name);
        }
        service.DialogFinished();
    }
}
