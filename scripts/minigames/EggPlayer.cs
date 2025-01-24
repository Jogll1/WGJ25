using Godot;
using System;

namespace WGJ25
{
	public partial class EggPlayer : CharacterBody2D
	{
		private int moveSpeed = 600;
		private float halfWidth = 16;

		public override void _Process(double delta)
		{
			Godot.Vector2 dir = new();
			if (Input.IsActionPressed("move_left")) dir.X -= 1;
			if (Input.IsActionPressed("move_right")) dir.X += 1;

			// Move
			GlobalPosition += dir * moveSpeed * (float)delta;

			// Clamp position
			GlobalPosition = new Godot.Vector2(
				Mathf.Clamp(GlobalPosition.X, halfWidth, GameManager.SCREEN_WIDTH - halfWidth),
				Mathf.Clamp(GlobalPosition.Y, 0, GameManager.SCREEN_HEIGHT)
			);
		}

		public static void OnBasketEntered(Node2D body)
		{
			if (body.IsInGroup("EggCollectable")) 
			{
				body.QueueFree();
			}
		}
	}
}