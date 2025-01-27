using Godot;
using System;

namespace WGJ25
{
	public partial class Hunter : CharacterBody2D
	{
		private int moveSpeed = 600;

        public override void _PhysicsProcess(double delta)
        {
			Vector2 dir = Input.GetVector("move_left", "move_right", "move_up", "move_down").Normalized();

			Velocity = dir * moveSpeed;
			MoveAndSlide();
        }
    }
}