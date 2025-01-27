using Godot;
using System;

namespace WGJ25{
	public partial class SnappyPlayer : CharacterBody2D
	{
		public const float Speed = 300.0f;
		public const float JumpVelocity = -400.0f;

		public override void _PhysicsProcess(double delta)
		{
			Vector2 velocity = Velocity;

			// Add the gravity.
			if (!IsOnFloor())
			{
				velocity += GetGravity() * (float)delta;
			}

			// Handle Jump.
			if (Input.IsActionJustPressed("move_up") && IsOnFloor())
			{
				//Make it so only one key needs to be pressed for jumping.
				velocity.Y = JumpVelocity;
			}

			// Get the input direction and handle the movement/deceleration.
			// As good practice, you should replace UI actions with custom gameplay actions.
			Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
			if (direction != Vector2.Zero)
			{
				velocity.X = direction.X * Speed;
			}
			else
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			}

			Velocity = velocity;
			MoveAndSlide();
		}
	}
}