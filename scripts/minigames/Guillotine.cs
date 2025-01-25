using Godot;
using System;

public partial class Guillotine : RigidBody2D
{
	private Godot.Vector2 originalPosition;
	private const int moveSpeed = 20;
	private Sprite2D sprite;
	private bool canBeDragged;
	private bool isBeingDragged;
	private bool isFalling;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		originalPosition = Position;
		sprite = GetNode<Sprite2D>("Sprite2D");
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
			dir.Normalized();
			this.GlobalPosition += new Vector2 (0, dir.Y * (float)delta * moveSpeed);
			if(Input.IsActionJustReleased("Click")){
				isBeingDragged = false;
				isFalling = true;
			}
			if((this.GlobalPosition.Y - originalPosition.Y) <= 10){
				Modulate = new Color(1, 1, 1);
				canBeDragged = false;
				isBeingDragged = false;
			}
		}
	}

	public override void _Input(InputEvent @event)
    {
		//If the sprite is clicked on, start falling.
        if(@event is InputEventMouseButton eventMouseButton && 
			Input.IsActionJustPressed("Click") && sprite.GetRect().HasPoint(ToLocal(eventMouseButton.Position))){
				GD.Print("You clicked on the guillotine");//For debugging
				
				//For now we just randomly change the colour of the sprite instead of loading a new one.
				Modulate = new Color(0, 0, 1);
				isFalling = true;
		}
		//If the mouse is being held and dragged, and it can be dragged, start dragging.
		else if (@event is InputEventMouseMotion eventMouseMotion && canBeDragged && 
				Input.IsActionPressed("Click") && sprite.GetRect().HasPoint(ToLocal(eventMouseMotion.Position))) {
				isBeingDragged = true;
		}
    }
}
