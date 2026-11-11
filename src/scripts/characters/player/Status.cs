using Godot;
using System;
namespace GreenProject.Characters.Player;

[GlobalClass]
public partial class Status : Resource
{
    [ExportGroup("Movement")]
    [Export]
    public float MaxSpeed {  get; private set; }

    [Export]
    public float Acceleration {  get; private set; }

    [Export]
    public float Brake {  get; private set; }

    public Status() { }
}
