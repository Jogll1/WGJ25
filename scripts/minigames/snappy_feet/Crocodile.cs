using Godot;
using System;

namespace WGJ25{
	public partial class Crocodile : CharacterBody2D
	{
		public bool IsSnappy {get {return isSnappy;} set {isSnappy = value;}}
		public bool ShouldJump {get {return shouldJump;}}
		public Timer timer;
		private bool isSnappy;
		private bool shouldJump = false;
		private bool changeSprite = false;
		private Vector2 destination;
		private CollisionShape2D hurtbox;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			timer = GetNode<Timer>("Timer");
			hurtbox = GetNode<CollisionShape2D>("Hurtbox/CollisionShape2D");
			destination = new Vector2(GlobalPosition.X, GlobalPosition.Y - 100);
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			if(isSnappy){
				Modulate = new Color(1, 0, 0);
				hurtbox.Disabled = false;
				changeSprite = false;
			}
			else {
				hurtbox.Disabled = true;
				if(changeSprite) Modulate = new Color(0, 0, 1);
				else Modulate = new Color(0, 1, 0);
			}
		}
        public override void _PhysicsProcess(double delta)
        {
			//This works. Now need to add the game stopping when the player is hit
			//And area 2Ds to switch off the player collisions and make them fall directly.
            if(shouldJump){
				if(Math.Abs(GlobalPosition.Y - destination.Y) > 10) {
					Vector2 velocity = (destination - GlobalPosition) * 0.1f;
					MoveAndCollide(velocity);
				}
			}
        }

        public void OnArea2dBodyEntered(Node2D body){
			if(isSnappy) shouldJump = true;
		}

		public void OnTimerTimeout(){
			if(!isSnappy) changeSprite = true;
		}
	}
}