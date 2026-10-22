class_name Opponent extends CombatActor
@onready var _timer: Timer = Timer.new()

@onready var current_health: float = stats.max_health

var attack_text: String = "%s attacked %s dealing %s amount of damage."

func _ready() -> void:
	add_child(_timer)
	_timer.wait_time = 0.25

func start_turn() -> void:
	_timer.start()
	await _timer.timeout
	attack($"../Player")

func attack(target: CombatActor) -> void:
	print(attack_text % [stats.name,target.name, stats.damage])
	target.take_damage(stats.damage)
	turn_finished.emit()

func take_damage(amount: float) -> void:
	current_health-=amount
	if current_health <= 0:
		dead.emit()
