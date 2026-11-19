using Godot;
using GreenProject.Dialog;
using System;
namespace GreenProject;

public partial class EventsManager : Node
{
    // Dialog
    [Signal]
    public delegate void DialogStartedEventHandler(IDialog dialog);
    [Signal]
    public delegate void DialogBoxOpenedEventHandler();
    [Signal]
    public delegate void DialogBoxClosedEventHandler();

    public static EventsManager Instance { get; private set; }
    public override void _Ready()
    {
        Instance = this;
    }
}
