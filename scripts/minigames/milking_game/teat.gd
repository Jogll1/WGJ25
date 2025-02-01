extends Sprite2D


var dragging = false
var reset = false
var of = Vector2(0,0)

@export var manager_node: Node

var snap = 25
var maxY = 0
func _process(_delta):
	if reset:
		position = Vector2(position.x, position.y - 0.8)
		if position.y <= 200:
			position = Vector2(position.x, 200)
			reset = false
			maxY = 0
	if dragging:
		var newPos = get_global_mouse_position() - of
		maxY = snapped (newPos.y,snap)
		if maxY > 400:
			maxY = 400
			dragging = false
			reset = true
			manager_node.IncrementCount()
		if maxY < 200:
			maxY = 200
		position = Vector2(position.x,maxY)



func _on_button_button_down():
	if maxY != 400:
		dragging = true
	of = get_global_mouse_position() - global_position


func _on_button_button_up():
	dragging = false
