using Godot;
using System;

namespace WGJ25
{
	public partial class MainMenu : Node2D
	{
		private GameManager gameManager;

		private ParallaxBackground parallaxBackground;

		public override void _Ready()
		{
			gameManager = GetNode<GameManager>("/root/GameManager");
			parallaxBackground = GetNode<ParallaxBackground>("ParallaxBackground");
		}

        public override void _Process(double delta)
        {
            parallaxBackground.ScrollOffset += new Godot.Vector2(-0.5f, 0);
        }

        public void OnPlayPressed()
		{
			gameManager.LoadNextGame();
		}

		public void OnQuitPressed()
		{
			GetTree().Quit();
		}
	}
}