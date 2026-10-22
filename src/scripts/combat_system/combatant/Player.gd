class_name Player extends CombatActor

@onready var current_health: float = stats.max_health

var is_active: bool = false

var attack_text: String = "%s attacked %s dealing %s amount of damage."

func start_turn() -> void:
	is_active = true

func attack(target: CombatActor) -> void:
	if is_active:
		print(attack_text % [stats.name,target.name, stats.damage])
		target.take_damage(stats.damage)
		end_turn()
	else:
		return

func take_damage(amount: float) -> void:
	current_health-=amount
	if current_health <= 0:
		dead.emit()

func end_turn() -> void:
	is_active=false
	turn_finished.emit()

func _on_attack_pressed() -> void:
	attack($"../Opponent")
