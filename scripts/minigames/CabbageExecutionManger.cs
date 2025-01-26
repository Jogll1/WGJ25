using Godot;
using System;
using System.Runtime.CompilerServices;
using WGJ25;

public partial class CabbageExecutionManger : MinigameManager
{
	private Executable executable;
	private Guillotine guillotine;
	private StaticBody2D board;
	private int cabbageCount = 0;
	private int nobleCount = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Setting initial positions of objects in the scene
		executable = GetNode<Executable>("Executable");
		if(executable != null) executable.GlobalPosition = new Vector2(GameManager.SCREEN_WIDTH / 2, GameManager.SCREEN_HEIGHT / 1.5f);

		guillotine = GetNode<Guillotine>("Guillotine");
		if(guillotine != null) guillotine.GlobalPosition = new Vector2(GameManager.SCREEN_WIDTH / 2, GameManager.SCREEN_HEIGHT / 5); 

		board = GetNode<StaticBody2D>("Board");
		if(board != null) board.GlobalPosition = new Vector2(GameManager.SCREEN_WIDTH / 2, executable.GlobalPosition.Y + 40);
	}

    public override void _Process(double delta)
    {
		//To avoid double-clicking that sometimes occurs when dragging the guillotine back up.
		if(guillotine.CanBeDragged) executable.CanBeClicked = false;
		else executable.CanBeClicked = true;

		if(guillotine.IsFalling && executable.IsCabbage) cabbageCount++;
		else if (guillotine.IsFalling && !executable.IsCabbage) nobleCount++;

		GD.Print("Cabbages: " + cabbageCount + " Nobles: " + nobleCount);
    }
}
