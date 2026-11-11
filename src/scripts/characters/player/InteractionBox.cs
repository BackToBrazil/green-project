using Godot;
using System;
namespace GreenProject.Characters.Player;

public partial class InteractionBox : Area2D
{
    [Export]
    private CharacterBody2D _characterBody;
    [Export]
    public float Distance { get; private set; } = 42f;

    private Vector2 _direction = Vector2.Up;
    private Vector2 _lastDirection = Vector2.Up;

    public override void _PhysicsProcess(double delta)
    {
        _direction = _characterBody.Velocity.Normalized();
        if (_direction != Vector2.Zero)
        {
            _lastDirection = _direction;
            Position = _direction * Distance;
        }
        else
        {
            Position = _lastDirection * Distance;
        }
    }
}
