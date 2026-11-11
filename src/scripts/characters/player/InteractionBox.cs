using Godot;
using System;
using System.Threading.Tasks;
namespace GreenProject.Characters.Player;

public partial class InteractionBox : Area2D
{
    [Export]
    private CharacterBody2D _characterBody;
    [Export]
    private CollisionShape2D _collisionShape;
    [Export]
    public float Distance { get; private set; } = 42f;

    private Vector2 _direction = Vector2.Up;
    private Vector2 _lastDirection = Vector2.Up;
    private bool _isEnabled = false;
    private Timer _timer;

    public override void _Ready()
    {
        _timer = new Timer();
        AddChild(_timer);
    }

    public override void _PhysicsProcess(double delta)
    {
        //_direction = _characterBody.Velocity.Normalized();
        _direction = Input.GetVector("left", "right", "up", "down");
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

    public async Task TryInteract()
    {
        if (!_isEnabled)
        {
            _isEnabled = true;
            _timer.Start(0.1f);
            _collisionShape.SetDeferred("disabled", false);
            GD.Print("ENADLED");
            await ToSignal(_timer, Timer.SignalName.Timeout);
            _collisionShape.SetDeferred("disabled", true);
            GD.Print("DISABLED");
            _isEnabled = false;
        }
        return;
    }
}
