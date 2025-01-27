using Godot;
using System;

namespace WGJ25
{
	public partial class HuntingGameManager : MinigameManager
	{
		public override void _Ready()
		{
			base._Ready();

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