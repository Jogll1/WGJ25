using Godot;
using System;

namespace WGJ25
{
	public partial class Hunter : CharacterBody2D
	{
		private const string SPEAR_PATH = "res://scenes/minigames/hunting_game/spear.tscn";

		private HuntingGameManager huntingGameManager;
		private Timer timer;

		private int moveSpeed = 600;
		private bool canFire = true;
		private Godot.Vector2 lastDir = new(1, 0);

        public override void _Ready()
        {
            huntingGameManager = (HuntingGameManager)GetParent();
			timer = GetNode<Timer>("Timer");
        }

        public override void _Process(double delta)
        {
			if (!huntingGameManager.GameEnded && Input.IsActionPressed("fire") && canFire) 
			{
				canFire = false;
				timer.Start();

				// Spawn spear
				Spear spear = (Spear)ObjectManager.SpawnObject(SPEAR_PATH, GlobalPosition, GetParent());
				spear.SetVelocity(lastDir);
			}
        }

        public override void _PhysicsProcess(double delta)
        {
			if (!huntingGameManager.GameEnded) 
			{
				Vector2 dir = Input.GetVector("move_left", "move_right", "move_up", "move_down").Normalized();
				if (dir != Godot.Vector2.Zero) lastDir = dir;

				Velocity = dir * moveSpeed;

				MoveAndSlide();
			}
        }

		public void OnTimerTimeout() 
		{
			canFire = true;
		}
    }
}