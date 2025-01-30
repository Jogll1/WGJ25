using Godot;
using System;

public partial class Guillotine : RigidBody2D
{
	public bool CanBeDragged {get {return canBeDragged;} set{canBeDragged = value;}}
	public bool IsFalling {get {return isFalling;}}
	private Godot.Vector2 originalPosition;
	private Sprite2D sprite;
	private StaticBody2D board;
	private Executable executable;
	private Node2D parent;
	private const int moveSpeed = 40;
	private bool canBeDragged;
	private bool isBeingDragged;
	private bool isFalling;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		parent = GetParent<Node2D>();
		sprite = GetNode<Sprite2D>("Sprite2D");
		board = parent.GetNode<StaticBody2D>("Board");
		executable = parent.GetNode<Executable>("Executable");
		originalPosition = Position;
		canBeDragged = false;
		isBeingDragged =false;
		isFalling = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//If the guillotine is falling, check when it collides to indicate that it can be dragged
		if(isFalling){
			var collision = MoveAndCollide(new Vector2(0, 1) * moveSpeed);
			if(collision != null){
				isFalling = false;
				canBeDragged = true;
				Modulate = new Color(1, 0, 0);
			}
		}
		//If it is being dragged, check if the mouse has been released, at that point start falling 
		//again. Also check if it is near the start position. If it is, stop and reset the dragging bools.
		else if (isBeingDragged){
			Godot.Vector2 dir = GetViewport().GetMousePosition() - this.Position;

			MoveAndCollide(new Vector2(0, -1) * (moveSpeed/2));
			//If the player messes up so the guillotine isn't stuck
			if(Input.IsActionJustReleased("move_up")){
				isBeingDragged = false;
				isFalling = true;
			}
			//Resetting once the guillotine roughly reaches its original position.
			if((this.GlobalPosition.Y - originalPosition.Y) <= 10){
				Modulate = new Color(1, 1, 1);
				canBeDragged = false;
				isBeingDragged = false;
				executable.GetNextExecutable();
			}
		}
	}

	public override void _Input(InputEvent @event)
    {
		//If the sprite is clicked on, start falling.
        if(Input.IsActionJustPressed("move_down") ){
				GD.Print("You clicked on the guillotine");//For debugging
				
				//For now we just randomly change the colour of the sprite instead of loading a new one.
				Modulate = new Color(0, 0, 1);
				isFalling = true;
		}
		//If the mouse is being held and dragged, and it can be dragged, start dragging.
		else if (canBeDragged && 
				Input.IsActionPressed("move_up")) {
				isBeingDragged = true;
		}
    }
}
