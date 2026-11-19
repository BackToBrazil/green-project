using Godot;
using System;
using Godot.Collections;
using System.Threading.Tasks;
namespace GreenProject.Dialog;

public abstract partial class IDialogService : RefCounted
{
    public abstract Task SayLine(string line, string name);
    public abstract void DialogFinished();
    public abstract Task<int> ShowChoices(Array<string> choices);
}
