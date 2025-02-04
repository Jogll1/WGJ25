using Godot;
using System;

namespace WGJ25
{
	public partial class Ending : Node2D
	{
		private ParallaxBackground parallaxBackground;
        public override void _Ready()
        {
            GetNode<GameManager>("/root/GameManager").PlayVictoryMusic();
			parallaxBackground = GetNode<ParallaxBackground>("ParallaxBackground");
        }

		public override void _Process(double delta)
        {
            parallaxBackground.ScrollOffset += new Godot.Vector2(-0.5f, 0);
        }

        public void OnMenuPressed()
		{
			GetNode<GameManager>("/root/GameManager").Home();
		}
	}
}