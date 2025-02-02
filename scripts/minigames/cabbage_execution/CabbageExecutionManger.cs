using Godot;
using System;


namespace WGJ25{
	public partial class CabbageExecutionManger : MinigameManager
	{
		private const string EXECUTABLE_PATH = "res://scenes/minigames/cabbage_execution/executable.tscn";
		private Executable executable;
		private Guillotine guillotine;
		private StaticBody2D board;
		private StaticBody2D basket;
		private int cabbageCount = 0;
		private int nobleCount = 0;
		
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			base._Ready();
			//Setting initial positions of objects in the scene
			executable = GetNode<Executable>("Executable");
			if(executable != null) executable.GlobalPosition = new Vector2(GameManager.SCREEN_WIDTH / 2, GameManager.SCREEN_HEIGHT / 1.5f);

			guillotine = GetNode<Guillotine>("Guillotine");
			if(guillotine != null) guillotine.GlobalPosition = new Vector2(GameManager.SCREEN_WIDTH / 2, GameManager.SCREEN_HEIGHT / 5); 

			board = GetNode<StaticBody2D>("Board");
			if(board != null) board.GlobalPosition = new Vector2(GameManager.SCREEN_WIDTH / 2, executable.GlobalPosition.Y + 40);

			basket = GetNode<StaticBody2D>("Basket");
			if(basket != null) basket.GlobalPosition = new Vector2(GameManager.SCREEN_WIDTH / 2, board.GlobalPosition.Y + 128);

			SetPopupText("Slice the Cabbage!");
		}

		public override void _Process(double delta)
		{
			base._Process(delta);
			//To avoid double-clicking that sometimes occurs when dragging the guillotine back up.
			if(guillotine.CanBeDragged){
				executable.CanBeClicked = false;
				if(!executable.Executed){
					executable.Executed = true;
					executable.ApplyImpulse(new Vector2(0, -250));
					executable.GravityScale = 1f;
				}
			}  
			else executable.CanBeClicked = true;

			if(guillotine.IsFalling && executable.IsCabbage) cabbageCount++;
			else if (guillotine.IsFalling && !executable.IsCabbage) nobleCount++;
			if(!guillotine.CanBeDragged && !guillotine.IsFalling && executable.Executed){
				executable = (Executable)ObjectManager.SpawnObject(EXECUTABLE_PATH, new Vector2(GameManager.SCREEN_WIDTH / 2, GameManager.SCREEN_HEIGHT / 1.5f), this);
			}
		}

		protected override void OnStopwatchTimeout()
		{
			base.OnStopwatchTimeout();

			int score = Mathf.FloorToInt((float)cabbageCount/(float)(cabbageCount+nobleCount) * 100);
			GD.Print($"Score: {score}");

			gameManager.LoadNextGame();
		}
	}
}
