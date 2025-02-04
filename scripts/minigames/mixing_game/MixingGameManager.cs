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
				Ingredient temp = (Ingredient)ObjectManager.SpawnObject(INGREDIENT_PATH, new Vector2((GameManager.SCREEN_WIDTH/2) + (i+1)*32 , (GameManager.SCREEN_HEIGHT/3) + 64), this);
				//temp.sprite.Texture = GD.Load<Texture2D>(ingredientTextures[i]);
				temp.index = i;
			}

			SetPopupText("Mix!");
			SetControlText("Left Click and Drag the mouse to crush the ingredients");
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