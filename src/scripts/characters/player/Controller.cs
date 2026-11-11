using Godot;
using Godot.Collections;
using GreenProject.Characters.Player;
using System;
using System.Linq;
namespace GreenProject.Characters;


public partial class Controller : Node
{
    enum MovementState
    {
        IDLE,
        WALKING,
    }

    [ExportGroup("Dependencies")]
    [Export]
    public Player.Status Status {  get; private set; }
    [Export]
    private CharacterBody2D _characterBody;
    [Export]
    private AnimatedSprite2D _animatedSprite;
    [Export]
    private InteractionBox _interactionBox;

    
    private Vector2 _direction = Vector2.Zero;
    private Vector2 _lastDirection = Vector2.Up;
    private Vector2 _desiredVelocity = Vector2.Zero;
    private Vector2 _currentVelocity = Vector2.Zero;
    private MovementState _movementState = MovementState.IDLE;

    public override void _Process(double delta)
    {
        Vector2 direction = _lastDirection;

        switch (_movementState)
        {
            case MovementState.IDLE:
                PlayAnimation("idle", direction);
                break;
            case MovementState.WALKING:
                PlayAnimation("walk", direction);
                break;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        _desiredVelocity = HandleDirection() * Status.MaxSpeed;

        if(_desiredVelocity.IsZeroApprox())
            _currentVelocity = _currentVelocity.MoveToward(_desiredVelocity, Status.Brake * (float)delta);
        else
            _currentVelocity = _currentVelocity.MoveToward(_desiredVelocity, Status.Acceleration * (float)delta);

        _characterBody.Velocity = _currentVelocity;
        _characterBody.MoveAndSlide();

        if(_direction != Vector2.Zero)
            _lastDirection = _direction;

        if(_currentVelocity > Vector2.Zero)
            _movementState = MovementState.WALKING;
        else
            _movementState = MovementState.IDLE;
    }

    public override void _UnhandledKeyInput(InputEvent @event)
    {
        if (@event.IsActionPressed("interact"))
        {
            Array<Area2D> areas = _interactionBox.GetOverlappingAreas();
            
        }
    }

    private Vector2 HandleDirection()
    {
        Vector2 new_direction = Input.GetVector("left", "right", "up", "down");
        if (new_direction.X != 0 && new_direction.Y != 0)
        {

            if (Input.IsActionJustPressed("left") || Input.IsActionJustPressed("right"))
            {
                new_direction.Y = 0;
            }
            else if (Input.IsActionJustPressed("up") || Input.IsActionJustPressed("down"))
            {
                new_direction.X = 0;
            }
            else
            {
                new_direction = _direction;
            }
        }
        _direction = new_direction.Normalized();
        return new_direction.Normalized();
    }

    private void PlayAnimation(string animation, Vector2 direction)
    {
        string dir = "_up";
        if(direction == Vector2.Left)
            dir = "_left";
        else if (direction == Vector2.Right)
            dir = "_right";
        else if (direction == Vector2.Up)
            dir = "_up";
        else if (direction == Vector2.Down)
            dir = "_down";

        _animatedSprite.Play(animation+dir);
    }
}
