using Godot;
using System;
namespace GreenProject;

public partial class GameState : Node
{
    public static GameState Instance { get; private set; }

    public bool IsDialogBoxOpen { get; private set; } = false;

    public override void _Ready()
    {
        Instance = this;

        EventsManager.Instance.DialogBoxOpened += () => { IsDialogBoxOpen = true; };
        EventsManager.Instance.DialogBoxClosed += () => { IsDialogBoxOpen = false; };
    }
}
