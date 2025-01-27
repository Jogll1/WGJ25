using Godot;
using System;

namespace WGJ25
{
	public partial class HuntingGameManager : MinigameManager
	{
		private Hunter hunter;

		public override void _Ready()
		{
			base._Ready();

			hunter = GetNode<Hunter>("Hunter");
			if (hunter != null) hunter.GlobalPosition = new Vector2(GameManager.SCREEN_WIDTH / 2, GameManager.SCREEN_HEIGHT / 2);

			SetPopupText("Hunt the dodos!");
		}

		public override void _Process(double delta)
		{
			
		}

		protected override void OnStopwatchTimeout()
		{
			base.OnStopwatchTimeout();

			gameManager.LoadNextGame();
		}
	}
}