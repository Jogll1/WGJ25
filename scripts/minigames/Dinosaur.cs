using Godot;
using System;
using System.Numerics;

namespace WGJ25
{
	public partial class Dinosaur : RigidBody2D
	{
		// Scene paths
		private readonly string EGG_SCENE_PATH = "res://scenes/minigames/egg_game/egg.tscn";

		private const int screenWidth = 1200;
		private const int width = 128;

		private const int moveSpeed = 300;
		private Godot.Vector2 currentDir = new(1, 0);

		private Node2D parentScene;

		private Timer spawnTimer;
		private Node2D spawnPos;
		private Sprite2D sprite;
		
		public override void _Ready()
		{
			parentScene = GetParent<Node2D>();

			spawnTimer = GetNode<Timer>("SpawnTimer");
			spawnPos = GetNode<Node2D>("Sprite2D/SpawnPos");
			sprite = GetNode<Sprite2D>("Sprite2D");
		}

		public override void _Process(double delta)
		{
			if (spawnTimer.TimeLeft == 0)
			{
				ObjectManager.SpawnObject(EGG_SCENE_PATH, spawnPos.GlobalPosition, parentScene);
				spawnTimer.Start();
			}

			// Move
			MoveDinosaur(delta);
		}
		
		private void MoveDinosaur(double delta)
		{
			// Move left and right, if touch screen bounds, reverse direction
			float offset = width / 2 + 15;
			if (GlobalPosition.X <= offset || GlobalPosition.X >= screenWidth - offset)
			{
				currentDir = new Godot.Vector2(-currentDir.X, currentDir.Y);
				sprite.Scale = new Godot.Vector2(-sprite.Scale.X, sprite.Scale.Y);
			}

			// Update position and velocity
			GlobalPosition += currentDir * moveSpeed * (float)delta;
		}

		public void OnTimerTimeout() 
		{
			// Load the egg scene
			ObjectManager.SpawnObject(EGG_SCENE_PATH, spawnPos.GlobalPosition, parentScene);
		}
	}
}