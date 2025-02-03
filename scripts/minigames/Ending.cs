using Godot;
using System;

namespace WGJ25
{
	public partial class Ending : Node2D
	{
        public override void _Ready()
        {
            GetNode<GameManager>("/root/GameManager").PlayVictoryMusic();
        }

        public void OnMenuPressed()
		{
			GetNode<GameManager>("/root/GameManager").Home();
		}
	}
}