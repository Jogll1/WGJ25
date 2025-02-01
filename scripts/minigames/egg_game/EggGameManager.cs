using Godot;
using System;

namespace WGJ25
{
	public partial class EggGameManager : MinigameManager
	{
		private Dinosaur dinosaur;
		private EggPlayer player;

		public override void _Ready()
		{
			base._Ready();

			dinosaur = GetNode<Dinosaur>("Dinosaur");
			if (dinosaur != null) dinosaur.GlobalPosition = new Vector2(GameManager.SCREEN_WIDTH / 2, 64);

			player = GetNode<EggPlayer>("EggPlayer");
			if (player != null) player.GlobalPosition = new Vector2(GameManager.SCREEN_WIDTH / 2, GameManager.SCREEN_HEIGHT);

			SetPopupText("Catch the eggs!");
		}
		
		protected override void OnStopwatchTimeout()
		{
			base.OnStopwatchTimeout();
			dinosaur.StopSpawning();

			int score = Mathf.FloorToInt((float)player.EggsCaught / (float)dinosaur.EggsDropped * 100);
			GD.Print($"Score: {score}");

			gameManager.LoadNextGame();
		}
	}
}
