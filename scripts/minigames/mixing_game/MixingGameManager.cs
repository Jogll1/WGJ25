using Godot;
using System;

namespace WGJ25
{
	public partial class MixingGameManager : MinigameManager
	{
		public override void _Ready()
		{
			base._Ready();

			SetPopupText("Mix!");
		}

		public override void _Process(double delta)
		{

		}

		protected override void OnStopwatchTimeout()
		{
			base.OnStopwatchTimeout();

			// gameManager.LoadNextGame();
		}
	}
}