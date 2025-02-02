using Godot;
using System;
using System.Diagnostics;

namespace WGJ25{
	public partial class SnappyFeetManager : MinigameManager
	{
		private const string CROC_PATH = "res://scenes/minigames/snappy_feet/crocodile.tscn";
		private Crocodile[] croc;
		private StaticBody2D leftShore;
		private StaticBody2D rightShore;
		private SnappyPlayer player;
		private Timer timer;
		private Area2D end;
		private bool endReached = false;
		private bool ended = false;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{	
			base._Ready();
			leftShore = GetNode<StaticBody2D>("LeftShore");
			if(leftShore != null) leftShore.GlobalPosition = new Vector2(0, GameManager.SCREEN_HEIGHT - 128);

			rightShore = GetNode<StaticBody2D>("RightShore");
			if(rightShore != null) rightShore.GlobalPosition = new Vector2(GameManager.SCREEN_WIDTH - 192,GameManager.SCREEN_HEIGHT - 32);

			player = GetNode<SnappyPlayer>("SnappyPlayer");
			if(player != null) player.GlobalPosition = new Vector2(32, GameManager.SCREEN_HEIGHT - 180);

			end = GetNode<Area2D>("End");
			if(end != null) end.GlobalPosition = new Vector2(rightShore.GlobalPosition.X, rightShore.GlobalPosition.Y - 64);

			timer = GetNode<Timer>("Timer");

			croc = new Crocodile[6];
			for(int i = 0; i < croc.Length; i++){
				Crocodile temp = (Crocodile)ObjectManager.SpawnObject(CROC_PATH, new Vector2(128*(i+1), GameManager.SCREEN_HEIGHT - 64), this);
				if(i % 2 == 0) temp.IsSnappy = true;
				else temp.IsSnappy = false;
				croc[i] = temp;
			}

			SetPopupText("Jump Over the Crocodiles!");
			SetControlText("Press W to jump over the Crocodiles\nThe best time to jump is right as the crocodile is tensing");
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			base._Process(delta);
			if(!player.IsDead && player.caught){
				GD.Print("This should be doing something");
				player.IsDead = true;
				player.Sleeping = false;
				player.collider.Disabled = true;
				player.ApplyImpulse(new Vector2(0, -200));
			}
			//Pause the timer so we have no more croc state changes when the player dies.
			if(player.IsDead){
				timer.Paused = true;
				// player.GlobalPosition = new Vector2(32, GameManager.SCREEN_HEIGHT - 180);
				// player.Sleeping = false;
			}
			//Ending the game if the player falls out of the map
			if(player.GlobalPosition.Y > GameManager.SCREEN_HEIGHT + 32 && !ended){
				ended = true;
				EndGame();
			}
		}

		public void OnTimerTimeout(){
			for(int i = 0; i < croc.Length; i++){
				croc[i].IsSnappy = !croc[i].IsSnappy;
				croc[i].timer.Start();
			}
		}

		public void OnEndEntered(Area2D area){
			endReached = true;
			GD.Print("You got to the end!");
		}

		protected override void OnStopwatchTimeout()
		{
			base.OnStopwatchTimeout();

			gameManager.LoadNextGame();
		}
	}
}