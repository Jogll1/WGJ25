using Godot;
using System;

namespace WGJ25
{
	public partial class MilkingGameManager : MinigameManager
	{
		int score = 0;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			base._Ready();
		}

        public override void _Process(double delta)
        {
            base._Process(delta);
        }

        public void IncrementCount()
		{
			score += 10;
		}
		
		protected override void OnStopwatchTimeout()
		{
			base.OnStopwatchTimeout();;
			if(score > 100) score = 100;
			GD.Print($"Score: {score}");

			gameManager.LoadNextGame();
		}
	}
}
