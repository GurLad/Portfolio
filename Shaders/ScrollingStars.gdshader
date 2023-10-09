shader_type canvas_item;

uniform float Speed;

void vertex() {
	UV.x += TIME * Speed;
}
