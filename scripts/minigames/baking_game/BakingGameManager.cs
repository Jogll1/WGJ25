using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WGJ25
{
	public partial class BakingGameManager : MinigameManager
	{
		private List<Knob> knobs;

		public override void _Ready()
		{
			base._Ready();

			knobs = GetTree().GetNodesInGroup("Knob").Cast<Knob>().ToList();
			SetPopupText("Bake!");
			SetControlText("Click the knobs to turn them.");
		}

		public override void _Process(double delta)
		{
			base._Process(delta);
		}

		protected override void OnStopwatchTimeout()
		{
			base.OnStopwatchTimeout();
			
			EndWholeGame();
		}
	}
}