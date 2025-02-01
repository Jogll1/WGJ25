using Godot;
using System;

namespace WGJ25{
	public partial class CrushedIngredient : RigidBody2D
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _PhysicsProcess(double delta)
		{
			MoveAndCollide(new Vector2(0, -10));
		}
	}
}