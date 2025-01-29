using Godot;
using System;

namespace WGJ25
{
	public partial class Dodo : CharacterBody2D
	{
		private HuntingGameManager huntingGameManager;

		private const float moveSpeed = 4f;
		private float minMoveTime = 1.5f;
		private float maxMoveTime = 4f;
		private int halfSize = 24;

		private readonly Random random = new();
		private Timer dirTimer;

		public override void _Ready()
		{
			huntingGameManager = (HuntingGameManager)GetParent();

			dirTimer = GetNode<Timer>("Timer");
			RandomDirection();
		}

		public override void _PhysicsProcess(double delta)
		{
			if (!huntingGameManager.GameEnded)
			{
				if (GlobalPosition.X + halfSize >= GameManager.SCREEN_WIDTH || GlobalPosition.X - halfSize <= 0) 
				{
					Velocity = new(-Velocity.X, Velocity.Y);
					dirTimer.WaitTime = (float)(random.NextDouble() * (maxMoveTime - minMoveTime) + minMoveTime);
				}

				if (GlobalPosition.Y + halfSize >= GameManager.SCREEN_HEIGHT || GlobalPosition.Y - halfSize <= 0) 
				{
					Velocity = new(Velocity.X, -Velocity.Y);
					dirTimer.WaitTime = (float)(random.NextDouble() * (maxMoveTime - minMoveTime) + minMoveTime);
				}

				GlobalPosition = new Godot.Vector2(
					Mathf.Clamp(GlobalPosition.X, halfSize, GameManager.SCREEN_WIDTH - halfSize),
					Mathf.Clamp(GlobalPosition.Y, halfSize, GameManager.SCREEN_HEIGHT - halfSize)
				);

				MoveAndCollide(Velocity);
			}
		}

		private void RandomDirection()
		{
			dirTimer.WaitTime = (float)(random.NextDouble() * (maxMoveTime - minMoveTime) + minMoveTime);
			
			do
			{
				Velocity = new Godot.Vector2(random.Next(-1, 2), random.Next(-1, 2)).Normalized() * moveSpeed;
			} while (Velocity == Godot.Vector2.Zero);

			dirTimer.Start();
		}

		private void OnTimerTimeout()
		{
			RandomDirection();
		}
	}
}