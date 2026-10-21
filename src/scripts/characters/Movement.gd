class_name Movement extends Node

@export var speed: float = 400;
@export var acceleration: float = 1600;
@export var brake: float = 800;

@onready var _character: CharacterBody2D = $"..";

var real_velocity: Vector2;
var input: Vector2;

func move_player(delta: float) -> void:
	var target_velocity: Vector2 = input * speed;"res://src/scripts/Movement.gd"
	real_velocity = real_velocity.move_toward(target_velocity, velocity_rate() * delta);
	_character.velocity = real_velocity;
	_character.move_and_slide();
	

func velocity_rate() -> float:
	return brake if input.is_zero_approx() else acceleration;

func _physics_process(delta: float) -> void:
	input = Input.get_vector("left","right","up","down");
	move_player(delta);
