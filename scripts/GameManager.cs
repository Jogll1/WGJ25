using Godot;
using System;
using System.Collections.Generic;

namespace WGJ25 
{
	public partial class GameManager : Node2D
	{
		// Array of all minigame scenes
		private readonly string[] scenes = {"egg_game/egg_game", "cabbage_execution/cabbage_execution", "hunting_game/hunting_game"};
		private string[] selectedScenes;

		// References
		SceneTransition sceneTransition;

		// Variables
		private const int GAMES_AMOUNT = 5; // How many games will be played before cooking
		public const int SCREEN_WIDTH = 1280;
		public const int SCREEN_HEIGHT = 720;

		private int nextGame = 0; // Index of the next game

		public override void _EnterTree()
		{
			sceneTransition = GetNode<SceneTransition>("SceneTransition");
		}

        public override void _Ready()
        {
			GD.Print("---- Selected Games -----");
            selectedScenes = SelectGames();

			for (int i = 0; i < selectedScenes.Length; i++)
			{
				GD.Print((i + 1).ToString() + ": " + selectedScenes[i]);
			}
			GD.Print("-------------------------");
        }

        public override void _Process(double delta) 
		{
			// To test, if space is pressed, load the next game
			if (Input.IsActionJustPressed("ui_select")) 
			{
				LoadNextGame();
			}
		}

		private string[] SelectGames() 
		{
			// Select the five games that will be played this turn
			Random random = new();
			int scenesLength = scenes.Length;

			// Copy scenes array into a list for selection
			List<string> scenesCopy = new(scenes);

			// Initialise return array
			string[] selected = new string[GAMES_AMOUNT];

			for (int i = 0; i < selected.Length; i++)
			{
				if (i >= scenesLength)
				{
					// Allow duplicates if there are less than five scenes in scenes
					selected[i] = scenes[random.Next(0, scenesLength)];
				}
				else 
				{
					// Else, pick a random unused scene
					int index = random.Next(0, scenesCopy.Count);
					selected[i] = scenesCopy[index];
					scenesCopy.RemoveAt(index);
				}
			}

			return selected;
		}

		public void LoadNextGame() 
		{
			if (nextGame < GAMES_AMOUNT)
			{
				GD.Print($"{nextGame + 1}. Loading {selectedScenes[nextGame]}");
				sceneTransition.ChangeScene($"res://scenes/minigames/{selectedScenes[nextGame]}.tscn");
				nextGame++;
			}
		}

		public void LoadRandomGame() 
		{
			Random random = new();
			int index = random.Next(scenes.Length);
			GD.Print($"Loading {scenes[index]}");
			sceneTransition.ChangeScene($"res://scenes/minigames/{scenes[index]}.tscn");
		}
	}
}
