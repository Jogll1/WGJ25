using Godot;
using System;

namespace WGJ25
{
	public partial class Knob : Area2D
	{
		private Sprite2D sprite;

		public override void _Ready()
		{
			sprite = GetNode<Sprite2D>("Sprite2D");
		}

        public override void _Input(InputEvent @event)
        {
            if (@event is InputEventMouseButton eventMouseButton && Input.IsActionJustPressed("Click") && sprite.GetRect().HasPoint(ToLocal(eventMouseButton.Position)))
			{
				GD.Print("Clicked");
			}
        }

        public override void _Process(double delta)
		{
			
		}
	}
}