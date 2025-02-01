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
		
		public void IncrementCount()
		{
			score += 20;
		}
		
		protected override void OnStopwatchTimeout()
		{
			base.OnStopwatchTimeout();;
			GD.Print($"Score: {score}");

			gameManager.LoadRandomGame();
		}
	}
}
