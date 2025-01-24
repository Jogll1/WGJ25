using Godot;
using System;

namespace WGJ25
{
	public partial class EggGameManager : Node2D
	{
		private GameManager gameManager;
		private Dinosaur dinosaur;
		private EggPlayer player;

		public override void _Ready()
		{
			gameManager = GetNode<GameManager>("/root/GameManager");

			dinosaur = GetNode<Dinosaur>("Dinosaur");
			if (dinosaur != null) dinosaur.GlobalPosition = new Vector2(GameManager.SCREEN_WIDTH / 2, 25);

			player = GetNode<EggPlayer>("EggPlayer");
			if (player != null) player.GlobalPosition = new Vector2(GameManager.SCREEN_WIDTH / 2, GameManager.SCREEN_HEIGHT - 32);
		}
	}
}