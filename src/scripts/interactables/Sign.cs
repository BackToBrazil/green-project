using Godot;
using Godot.Collections;
using System;
using System.Threading.Tasks;
namespace GreenProject.Interactable;

public partial class Sign : Node2D, Interactable
{
    [Export]
    public Json LinesJSON { get; private set; }

    
    [Export(PropertyHint.File, "*.json")]
    public string Test {  get; private set; }

    private Area2D _area;
    private Array<string> _lines;
    private string _name;

    public override void _Ready()
    {
        _area = GetNode<Area2D>("%Area2D");
        _area.AreaEntered += OnAreaEntered;
        
        var lines = ResourceLoader.Load<Json>(Test);

        var data_received = (Dictionary)lines.Data;
        GD.Print(data_received);
        _lines = (Array<string>)data_received["lines"];
        _name = (string)data_received["name"];
    }

    private void OnAreaEntered(Area2D area)
    {
        _ = Interact();
    }

    public async Task Interact()
    {
        if (_lines == null)
            return;

        bool hasContinuation = true;
        for (int i = 0; i < _lines.Count; i++)
        {
            if (i + 1 >= _lines.Count)
                hasContinuation = false;
            EventsManager.Instance.EmitSignal(EventsManager.SignalName.DialogStarted, _lines[i], _name, hasContinuation);
            await ToSignal(EventsManager.Instance, EventsManager.SignalName.DialogFinished);
        }
    }
}
