class_name TurnManager extends Node

@export var combatants_list: Node
var queue: Array[Node] = []
var current_combatant: CombatActor = null: set = _set_current
var turn_count: int = 1

var get_queue_append_text: String = "appended combatant %s to the queue."
var get_queue_queue_size_text: String = "current queue size: %s. setting %s as the current_combatant."
var play_turn_text: String = "current_combatant '%s' took turn and attacked."
var pass_turn_back_queue_text: String = "passing turn... %s moved to the back of the queue."
var pass_turn_new_current_combatant_text: String = "turn is with %s now, waiting action."

var _is_combat_finished: bool = false: set = _on_combat_finished

func initialize() -> void:
	var combat_scene: CombatController = $".."
	combat_scene.combat_finished.connect(_on_combat_finished)
	get_queue()
	play_turn()

func get_queue() -> void:
	queue.clear()
	var nodes: Array[Node] = combatants_list.get_children()
	for node: CombatActor in nodes:
		queue.append(node)
		#print(get_queue_append_text % node.name)
	if queue.size() > 0:
		current_combatant = queue[0]
		#print(get_queue_queue_size_text % [queue.size(), current_combatant])
	print("\n")

func play_turn() -> void:
	await current_combatant.turn_finished
	if not _is_combat_finished:
		pass_turn()
		#turn_count += 1
		print ("\n")
		play_turn()

func pass_turn() -> void:
	var old_combatant: Node = queue.pop_front()
	queue.append(old_combatant)
	#print(pass_turn_back_queue_text % old_combatant)
	current_combatant = queue[0]
	#print(pass_turn_new_current_combatant_text % current_combatant)
	print("\n")

func _set_current(new_combatant: CombatActor) -> void:
	current_combatant = new_combatant
	current_combatant.start_turn()

func _on_combat_finished(b: bool) -> void:
	_is_combat_finished = b
