using Godot;
using System;
using Godot.Collections;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
namespace GreenProject.Dialog;

[GlobalClass]
public partial class SimpleDialog : Node
{
    [Export(PropertyHint.File, "*.json")]
    public string Dialog { get; private set; }

    public override void _Ready()
    {
        Dictionary<string, Variant> data_received = (Dictionary<string, Variant>)ResourceLoader.Load<Json>(Dialog).Data;
        
        if(data_received.ContainsKey("lines") && data_received.ContainsKey("name"))
            GetNode<Area2D>("%Area2D").AreaEntered += (obj) => { _ = Say((Array<string>)data_received["lines"], (string)data_received["name"]); };
        else
            GD.PrintErr(string.Format("Could not read {0}'s Dialog.json, check for formatting error.", Name));
    }

    public async Task Say(Array<string> lines, string name)
    {
        if (lines == null || lines.Count <= 0)
        {
            GD.PrintErr(string.Format("{0}'s lines file '{1}' is null or empty.", name, lines));
            return;
        }

        for (int i = 0; i < lines.Count; i++)
        {
            EventsManager.Instance.EmitSignal(EventsManager.SignalName.LineSaid, lines[i], name);
            await ToSignal(EventsManager.Instance, EventsManager.SignalName.DialogAdvanced);
        }
        EventsManager.Instance.EmitSignal(EventsManager.SignalName.DialogFinished);
    }
}
