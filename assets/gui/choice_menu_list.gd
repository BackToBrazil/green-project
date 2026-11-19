extends MenuList
const MenuList = preload("res://addons/ps1_rpg_menu/menu/menu_list.gd")

func create_choice(text: String) -> void:
	var b: Button = add_item(text)
	b.size_flags_horizontal = Control.SIZE_EXPAND_FILL
	b.size_flags_vertical = Control.SIZE_EXPAND_FILL
	b.alignment = HORIZONTAL_ALIGNMENT_CENTER
	pass
