class_name CombatantsManager extends Node
var belligerents: Array[PackedScene] = []

func initialize(bell: Array[PackedScene]) -> void:
	belligerents = bell
	
	print("")
