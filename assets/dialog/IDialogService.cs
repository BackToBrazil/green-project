using Godot;
using System;
using System.Threading.Tasks;
namespace GreenProject.Dialog;

public abstract partial class IDialogService : RefCounted
{
    public abstract Task SayLine(string line, string name);
    public abstract void DialogFinished();
}
