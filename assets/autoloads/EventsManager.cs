using Godot;
using System;
namespace GreenProject;

public partial class EventsManager : Node
{
    [Signal]
    public delegate void DialogStartedEventHandler(string line, string name = "", bool hasContinuation = false);
    [Signal]
    public delegate void DialogFinishedEventHandler();

    public bool IsDialogOpen = false;

    public static EventsManager Instance { get; private set; }
    public override void _Ready()
    {
        Instance = this;
    }
}
