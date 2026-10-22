@abstract
class_name CombatActor extends Node
@export var stats: CombatantStats

signal turn_finished
signal dead(CombatActor)

var actor_name: String:
	get: return stats.name

@abstract func start_turn() -> void;
@abstract func attack(target: CombatActor) -> void;
@abstract func take_damage(amount: float) -> void;
