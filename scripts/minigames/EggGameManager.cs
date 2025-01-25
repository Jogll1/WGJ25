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
			dinosaur = GetNode<Dinosaur>("Dinosaur");
			if (dinosaur != null) dinosaur.GlobalPosition = new Vector2(GameManager.SCREEN_WIDTH / 2, 25);

			player = GetNode<EggPlayer>("EggPlayer");
			if (player != null) player.GlobalPosition = new Vector2(GameManager.SCREEN_WIDTH / 2, GameManager.SCREEN_HEIGHT - 32);
		}
	}
}