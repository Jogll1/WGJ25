using Godot;
using System;

namespace WGJ25
{
	public partial class HuntingGameManager : MinigameManager
	{
		private readonly string DODO_SCENE = "res://scenes/minigames/hunting_game/dodo.tscn";

		private Hunter hunter;
		public int DodosKilled;
		private int dodoNum = 8;

		public override void _Ready()
		{
			base._Ready();

			hunter = GetNode<Hunter>("Hunter");
			if (hunter != null) hunter.GlobalPosition = new Vector2(GameManager.SCREEN_WIDTH / 2, GameManager.SCREEN_HEIGHT / 2);

			SetPopupText("Club the dodos!");

			// Spawn the initial dodos
			Random random = new();
			for (int i = 0; i < dodoNum; i++)
			{
				ObjectManager.SpawnObject(
					DODO_SCENE, 
					new Vector2(random.Next(0, GameManager.SCREEN_WIDTH), random.Next(0, GameManager.SCREEN_HEIGHT)), 
					this
				);
			}
		}

		public override void _Process(double delta)
		{
			// if (DodosKilled == dodoNum)
			// {
			// 	OnStopwatchTimeout();
			// }
		}

		protected override void OnStopwatchTimeout()
		{
			base.OnStopwatchTimeout();

			int score = Mathf.FloorToInt((float)DodosKilled / (float)dodoNum * 100);
			GD.Print($"Score: {score}");

			gameManager.LoadNextGame();
		}
	}
}