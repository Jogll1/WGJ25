using Godot;
using System;

namespace WGJ25
{
	public partial class EggPlayer : CharacterBody2D
	{
		private EggGameManager eggGameManager;

		private int moveSpeed = 600;
		private float halfWidth = 16;

		public int EggsCaught { get; private set; }

		public override void _Ready()
		{
			eggGameManager = (EggGameManager)GetParent();
		}

		public override void _Process(double delta)
		{
			if(!eggGameManager.GameEnded) 
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
		}

		private void OnBasketEntered(Node2D body)
		{
			if (body.IsInGroup("EggCollectable")) 
			{
				// Increment egg counter if it was an egg
				if (body.IsInGroup("Egg")) 
				{
					EggsCaught++;
				}
				else 
				{	
					// Catching a poop decreases score
					if(EggsCaught > 0) EggsCaught--;
				}

				body.QueueFree();
			}
		}
	}
}
