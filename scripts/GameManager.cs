using Godot;
using System;

namespace WGJ25 
{
	public partial class GameManager : Node2D
	{
		// Test
		private string[] scenes = {"egg_game/egg_game"};

		// References
		SceneTransition sceneTransition;

		public const int SCREEN_WIDTH = 1280;
		public const int SCREEN_HEIGHT = 720;

		public override void _EnterTree()
		{
			sceneTransition = GetNode<SceneTransition>("SceneTransition");
		}

		public override void _Process(double delta) 
		{
			// To test, if space is pressed, load a random game
			if (Input.IsActionJustPressed("ui_select")) 
			{
				LoadRandomGame();
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