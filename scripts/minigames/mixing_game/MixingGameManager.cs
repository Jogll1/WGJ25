using Godot;
using System;

namespace WGJ25
{
	public partial class MixingGameManager : MinigameManager
	{
		private const string INGREDIENT_PATH = "res://scenes/minigames/mixing_game/Ingredient.tscn";
		private Ladle ladle;
		private StaticBody2D bowl;
		public override void _Ready()
		{
			base._Ready();

			ladle = GetNode<Ladle>("Ladle");
			if(ladle != null) ladle.GlobalPosition = new Vector2(GameManager.SCREEN_WIDTH/2, GameManager.SCREEN_HEIGHT/4);

			bowl = GetNode<StaticBody2D>("Bowl");
			if(bowl != null) bowl.GlobalPosition = new Vector2(GameManager.SCREEN_WIDTH/2, GameManager.SCREEN_HEIGHT/1.3f);
			
			for(int i = 0; i < 5; i++){
				ObjectManager.SpawnObject(INGREDIENT_PATH, new Vector2((GameManager.SCREEN_WIDTH/3) + (i+1)*32 , (GameManager.SCREEN_HEIGHT/3) + 64), this);
			}

			SetPopupText("Mix!");
		}

		public override void _Process(double delta)
		{
			base._Process(delta);
		}

		protected override void OnStopwatchTimeout()
		{
			base.OnStopwatchTimeout();
			gameManager.loadLastGame();
			// gameManager.LoadNextGame();
		}
	}
}