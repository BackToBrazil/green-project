class_name CombatController extends Node2D

func _ready() -> void:
	var turn_manager: TurnManager = $TurnManager
	turn_manager.initialize()
