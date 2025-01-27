using Godot;
using System;

namespace WGJ25{
	public partial class SnappyFeetManager : MinigameManager
	{
		private const string CROC_PATH = "res://scenes/minigames/snappy_feet/crocodile.tscn";
		private Crocodile[] croc;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			croc = new Crocodile[6];
			for(int i = 0; i < croc.Length; i++){
				Crocodile temp = (Crocodile)ObjectManager.SpawnObject(CROC_PATH, new Vector2(GameManager.SCREEN_WIDTH + (32*(i+1)), GameManager.SCREEN_HEIGHT - 64), this);
				if(i % 2 == 0) temp.IsSnappy = false;
				else temp.IsSnappy = true;
				croc[i] = temp;
			}
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
	}
}