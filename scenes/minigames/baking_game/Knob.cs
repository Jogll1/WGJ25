using Godot;
using System;

namespace WGJ25
{
	public partial class Knob : Sprite2D
	{
		public override void _Ready()
		{

		}

        public override void _Input(InputEvent @event)
        {
            if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
			{
				Vector2 mousePos = GetGlobalMousePosition();
				if (GetRect().HasPoint(mousePos))
				{
					GD.Print("Sprite Clicked");
				}
			}
        }

        public override void _Process(double delta)
		{
			
		}
	}
}