class_name Combatant extends Node2D
@export var damage: float = 1
@export var health: float = 3
@export var target: Combatant
@onready var _timer: Timer = Timer.new()
signal turn_finished
signal died(dead: Combatant)

var attack_text: String = "%s is attacking %s dealing %s damage."
var take_damage_text: String = "%s is taking %s amount of damage. 
Its current Health is %s"

func _ready() -> void:
	add_child(_timer)
	_timer.wait_time = 0.25

func take_turn() -> void:
	_timer.start()
	await _timer.timeout
	attack()

func attack() -> void:
	#print(attack_text % [name, target.name, damage])
	target.take_damage(damage)
	turn_finished.emit()

func take_damage(amount_of_damage: float) -> void:
	health -= amount_of_damage
	print(take_damage_text % [name, amount_of_damage, health])
	if(health <= 0):
		died.emit()
