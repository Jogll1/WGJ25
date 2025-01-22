using Godot;
using System;

namespace WGJ25 
{
	public partial class GameManager : Node2D
	{
		// Test
		private string[] scenes = new string[3] {"game1", "game2", "game3"};

		// References
		SceneTransition sceneTransition;

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
			Random random = new Random();
			int index = random.Next(scenes.Length);
			GD.Print($"Loading {scenes[index]}");
			sceneTransition.ChangeScene($"res://scenes/minigames/{scenes[index]}.tscn");
		}
	}
}