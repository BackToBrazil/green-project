using Godot;
using System;
namespace GreenProject;

public partial class EventsManager : Node
{
    [Signal]
    public delegate void DialogFinishedEventHandler();
    [Signal]
    public delegate void DialogBoxOpenedEventHandler();
    [Signal]
    public delegate void DialogBoxClosedEventHandler();
    [Signal]
    public delegate void LineSaidEventHandler(string line, string name = "");
    [Signal]
    public delegate void DialogAdvancedEventHandler();

    public static EventsManager Instance { get; private set; }
    public override void _Ready()
    {
        Instance = this;
    }
}
