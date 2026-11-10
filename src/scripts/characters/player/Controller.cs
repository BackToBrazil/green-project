using Godot;
using System;
namespace GreenProject.Characters;


public partial class Controller : Node
{
    [ExportGroup("Dependencies")]
    [Export]
    public Player.Status Status {  get; private set; }
    [Export]
    private CharacterBody2D _characterBody;
    [Export]
    private AnimatedSprite2D _animatedSprite;

    
    private Vector2 _direction = Vector2.Zero;
    private Vector2 _lastDirection = Vector2.Up;
    private Vector2 _desiredVelocity = Vector2.Zero;
    private Vector2 _currentVelocity = Vector2.Zero;

    public override void _Ready()
    {
        GD.Print(string.Format("MaxSpeed: {0}\nAcceleration: {1}\nBrake: {2}", Status.MaxSpeed, Status.Acceleration, Status.Brake));
    }

    public override void _Process(double delta)
    {
        
    }

    public override void _PhysicsProcess(double delta)
    {
        _direction = HandleDirection();

        _desiredVelocity = _direction * Status.MaxSpeed;

        if(_desiredVelocity.IsZeroApprox())
            _currentVelocity = _currentVelocity.MoveToward(_desiredVelocity, Status.Brake * (float)delta);
        else
            _currentVelocity = _currentVelocity.MoveToward(_desiredVelocity, Status.Acceleration * (float)delta);

        _characterBody.Velocity = _currentVelocity;
        _characterBody.MoveAndSlide();
    }

    public override void _UnhandledKeyInput(InputEvent @event)
    {
        
    }

    private Vector2 HandleDirection()
    {
        Vector2 direction = Input.GetVector("left", "right", "up", "down");
        if (direction.X != 0 && direction.Y != 0)
        {

            if (Input.IsActionJustPressed("left") || Input.IsActionJustPressed("right"))
            {
                direction.Y = 0;
            }
            else if (Input.IsActionJustPressed("up") || Input.IsActionJustPressed("down"))
            {
                direction.X = 0;
            }
            else
            {
                direction = _lastDirection;
            }
        }
        _lastDirection = direction.Normalized();
        return direction.Normalized();
    }
}
