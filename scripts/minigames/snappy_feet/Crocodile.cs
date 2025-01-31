using Godot;
using System;

namespace WGJ25{
	public partial class Crocodile : CharacterBody2D
	{
		public bool IsSnappy {get {return isSnappy;} set {isSnappy = value;}}
		public bool ShouldJump {get {return shouldJump;}}
		private bool isSnappy;
		private bool shouldJump = false;
		private Vector2 destination;
		private CollisionShape2D hurtbox;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			hurtbox = GetNode<CollisionShape2D>("Hurtbox/CollisionShape2D");
			destination = new Vector2(GlobalPosition.X, GlobalPosition.Y - 100);
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			if(isSnappy){
				Modulate = new Color(1, 0, 0);
				//hurtbox.collider.disabled
			}
			else {
				Modulate =  new Color(0, 1, 0);
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
			GD.Print("Something is happening");
			if(isSnappy) shouldJump = true;
		}
	}
}