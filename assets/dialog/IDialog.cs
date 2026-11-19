using Godot;
using System;
using System.Threading.Tasks;
namespace GreenProject.Dialog;

public abstract partial class IDialog : Node
{
    public abstract Task SayLines(IDialogService service);
}
