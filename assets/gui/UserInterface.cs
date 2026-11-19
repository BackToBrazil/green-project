using Godot;
using GreenProject.Dialog;
using System;
namespace GreenProject.UI;

public partial class UserInterface : Control
{
    [Export]
    public PanelContainer DialogBox {  get; private set; }
    [Export]
    public PanelContainer ChoiceMenu { get; private set; }
    [Export]
    public GridContainer ChoiceGrid { get; private set; }

    private DialogUserInterfaceAdapter _dialogAdapter = null;
    public override void _Ready()
    {
        _dialogAdapter = new(this);
    }
}
