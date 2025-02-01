using Godot;
using System;
using System.Numerics;

namespace WGJ25
{
	public partial class Dinosaur : RigidBody2D
	{
		// Scene paths
		private const string EGG_SCENE_PATH = "res://scenes/minigames/egg_game/egg.tscn";
		private const string POOP_SCENE_PATH = "res://scenes/minigames/egg_game/poop.tscn";

		// Variables
		private const int width = 128;
		private const int moveSpeed = 350;
		private Godot.Vector2 currentDir = new(1, 0);
		private bool spawnedPoop = false;

		public int EggsDropped { get; private set; }

		// References
		private GameManager gameManager;
		private EggGameManager eggGameManager;
		private Timer spawnTimer;
		private Node2D spawnPos;
		private Sprite2D sprite;
		
		public override void _Ready()
		{
			// Get references
			gameManager = GetNode<GameManager>("/root/GameManager");
			eggGameManager = (EggGameManager)GetParent();
			spawnTimer = GetNode<Timer>("SpawnTimer");
			spawnPos = GetNode<Node2D>("Sprite2D/SpawnPos");
			sprite = GetNode<Sprite2D>("Sprite2D");
		}

		public override void _Process(double delta)
		{
			if (spawnTimer.TimeLeft == 0 && !eggGameManager.GameEnded && spawnTimer.TimeLeft + 1.25f < eggGameManager.GetTimerTimeLeft())
			{
				Random random = new();
				if (random.NextDouble() < 0.33f && !spawnedPoop)
				{
					// Load the poop scene
					ObjectManager.SpawnObject(POOP_SCENE_PATH, spawnPos.GlobalPosition, eggGameManager);
					spawnedPoop = true;
				}
				else
				{
					// Load the egg scene
					ObjectManager.SpawnObject(EGG_SCENE_PATH, spawnPos.GlobalPosition, eggGameManager);
					EggsDropped++;
					spawnedPoop = false;
				}

				spawnTimer.WaitTime = (float)(random.NextDouble() * (1.5f - 0.85f) + 0.85f);
				spawnTimer.Start();
			}

			// Move
			MoveDinosaur(delta);
		}
		
		private void MoveDinosaur(double delta)
		{
			if(!eggGameManager.GameEnded) 
			{
				// Move left and right, if touch screen bounds, reverse direction
				float offset = width / 2 + 64;
				if (GlobalPosition.X <= offset || GlobalPosition.X >= GameManager.SCREEN_WIDTH - offset)
				{
					currentDir = new Godot.Vector2(-currentDir.X, currentDir.Y);
					sprite.Scale = new Godot.Vector2(-sprite.Scale.X, sprite.Scale.Y);
				}

				// Update position and velocity
				GlobalPosition += currentDir * moveSpeed * (float)delta;
			}
		}

		public void StopSpawning() 
		{
			spawnTimer.Stop();
		}

		public void OnTimerTimeout() 
		{
			// Load the egg scene
			ObjectManager.SpawnObject(EGG_SCENE_PATH, spawnPos.GlobalPosition, eggGameManager);
		}
	}
}
