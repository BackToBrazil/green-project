class_name CombatController extends Node2D
@export var combatants_list: Node

signal combat_finished(bool)

var _on_combatant_death_text: String = "%s has died."

func _ready() -> void:
	for node: CombatActor in combatants_list.get_children():
		node.dead.connect(_on_combatant_death.bind(node))
	
	var turn_manager: TurnManager = $TurnManager
	turn_manager.initialize()

func _on_combatant_death(combatant: CombatActor) -> void:
	print(_on_combatant_death_text % combatant.actor_name)
	combat_finished.emit(true)
