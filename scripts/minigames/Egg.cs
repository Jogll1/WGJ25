using Godot;
using System;

namespace WGJ25
{
	public partial class Egg : RigidBody2D
	{
		private AnimatedSprite2D animatedSprite2D;

		public override void _Ready()
		{
			animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
			animatedSprite2D.Play("default");
		}

		private void OnBodyEntered(Node body)
		{
			Random random = new();
			animatedSprite2D.Scale = new Godot.Vector2(
				animatedSprite2D.Scale.X * (random.Next(0, 2) == 0 ? -1 : 1),
				animatedSprite2D.Scale.Y
			);
			animatedSprite2D.Play("cracked");
			SetDeferred("freeze", true);
			GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
		}
	}
}