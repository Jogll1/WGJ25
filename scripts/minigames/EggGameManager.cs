using Godot;
using System;

namespace WGJ25
{
	public partial class EggGameManager : Node2D
	{
		private GameManager gameManager;
		private Dinosaur dinosaur;

		public override void _Ready()
		{
			gameManager = GetNode<GameManager>("/root/GameManager");

			dinosaur = GetNode<Dinosaur>("Dinosaur");
			dinosaur.GlobalPosition = new Vector2(600, 25);
		}

		public override void _Process(double delta)
		{

		}
	}
}