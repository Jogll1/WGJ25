using Godot;
using System;

namespace WGJ25
{
	public partial class Knob : Control
	{
		private const float degrees = Mathf.Pi / 4;
		private readonly Random random = new();
		private BakingGameManager bakingGameManager;

		private TextureButton knobButton;
		private Sprite2D knobSettings;
		private float targetAngle = 0;
		private Timer timer;

		public bool OnTarget = false;

		public override void _Ready()
		{
			bakingGameManager = (BakingGameManager)GetParent().GetParent().GetParent().GetParent();
			knobButton = GetNode<TextureButton>("Knob");
			timer = GetNode<Timer>("Timer");
			knobSettings = GetNode<Sprite2D>("KnobSettings");

			targetAngle = random.Next(3, 7) * degrees;
			knobSettings.SetRotation(targetAngle);
		}

		public override void _Process(double delta)
		{
			OnTarget = knobButton.Rotation == targetAngle;
		}

		public void OnKnobDown()
		{
			knobButton.SetRotation(knobButton.Rotation + degrees);

			timer.WaitTime = (float)(random.NextDouble() * (2f - 1.25f) + 1.25f);
			timer.Start();
		}

		public void OnTimerTimeout()
		{
			if(!bakingGameManager.GameEnded && bakingGameManager.GetTimerTimeLeft() > 1.25f)
			{
				if (knobButton.Rotation != 0) knobButton.SetRotation(knobButton.Rotation - degrees);

				timer.WaitTime = (float)(random.NextDouble() * (2f - 1.25f) + 1.25f);
				timer.Start();
			}
		}
	}
}
