using Godot;
using System;
namespace GreenProject;

public partial class EventsManager : Node
{
    [Signal]
    public delegate void InteractionStartedEventHandler(Node2D origin);
    [Signal]
    public delegate void InteractionFinishedEventHandler(Node2D origin);

    public bool IsDialogOpen = false;

    public static EventsManager Instance { get; private set; }
    public override void _Ready()
    {
        Instance = this;
    }
}
