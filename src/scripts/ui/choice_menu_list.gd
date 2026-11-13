extends MenuList
const MenuList = preload("res://addons/ps1_rpg_menu/menu/menu_list.gd")

func _ready() -> void:
	var b1: Button = add_item("Loli hentai loli honorifics sushi hikikomori inuyasha miyazaki megane oneesan.")
	b1.size_flags_horizontal = Control.SIZE_EXPAND_FILL
	b1.size_flags_vertical = Control.SIZE_EXPAND_FILL
	b1.alignment = HORIZONTAL_ALIGNMENT_CENTER
	
	var b2: Button = add_item("Dango hai dub seinan green tea yuri mecha moe anime hentai.")
	b2.size_flags_horizontal = Control.SIZE_EXPAND_FILL
	b2.size_flags_vertical = Control.SIZE_EXPAND_FILL
	b2.alignment = HORIZONTAL_ALIGNMENT_CENTER
	
	
