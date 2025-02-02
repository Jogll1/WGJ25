using Godot;
using System;

namespace WGJ25 {
	public partial class Ladle : CharacterBody2D
	{
		private Sprite2D sprite;
		private bool isBeingDragged = false;
		private bool isFalling;
		private const int moveSpeed = 20;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			sprite = GetNode<Sprite2D>("Sprite2D");
			ProcessMode = Node.ProcessModeEnum.Pausable;
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _PhysicsProcess(double delta)
		{
			if(isFalling){
				MoveAndCollide(new Vector2(0, 0.5f) * moveSpeed);
			}
			else if (isBeingDragged){
				Godot.Vector2 dir = GetViewport().GetMousePosition() - this.GlobalPosition;
				dir.Normalized();
				Velocity = dir*moveSpeed;
				MoveAndSlide();
				if(Input.IsActionJustReleased("click")){
					isBeingDragged = false;
					isFalling = true;
				}	
			}
		}

		public override void _Input(InputEvent @event)
		{
			if(@event is InputEventMouseMotion eventMotion && Input.IsActionPressed("click")
				&& sprite.GetRect().HasPoint(ToLocal(eventMotion.Position))){
					isFalling = false;
					isBeingDragged = true;
			}
		}
	}
}