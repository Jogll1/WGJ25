using Godot;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace WGJ25{
	public partial class Crocodile : CharacterBody2D
	{
		public bool IsSnappy {get {return isSnappy;} set {isSnappy = value;}}
		public bool ShouldJump {get {return shouldJump;}}
		public Timer timer;
		private bool isSnappy;
		private bool shouldJump = false;
		private bool goDown = false;
		private bool changeSprite = false;
		private Vector2 destination;
		private Vector2 startPosition;
		private CollisionShape2D hurtbox;
		private Area2D area;
		private Sprite2D sprite;
		private Texture2D closed;
		private Texture2D tense;
		private Texture2D open;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			timer = GetNode<Timer>("Timer");
			area = GetNode<Area2D>("Area2D");
			hurtbox = GetNode<CollisionShape2D>("Hurtbox/CollisionShape2D");
			sprite = GetNode<Sprite2D>("Sprite2D");
			closed = GD.Load<Texture2D>("res://art/minigames/snappy/CrocodileClosed.png");
			tense = GD.Load<Texture2D>("res://art/minigames/snappy/CrocodileTensed.png");
			open = GD.Load<Texture2D>("res://art/minigames/snappy/CrocodileOpen.png");
			startPosition = GlobalPosition;
			destination = new Vector2(GlobalPosition.X, GlobalPosition.Y - 80);
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			if(isSnappy){
				//Modulate = new Color(1, 0, 0);
				sprite.Texture = open;
				hurtbox.Disabled = false;
				changeSprite = false;
			}
			else {
				hurtbox.Disabled = true;
				if(changeSprite) sprite.Texture = tense;//Modulate = new Color(0, 0, 1);
				else sprite.Texture = closed;//Modulate =new Color(0, 1, 0);
			}

			foreach(Node2D a in area.GetOverlappingAreas()){
				//GD.Print($"{a.Name}");
				if(a.IsInGroup("player") && isSnappy){
					shouldJump = true;
				}
			}
		}
        public override void _PhysicsProcess(double delta)
        {
			//This works. Now need to add the game stopping when the player is hit
			//And area 2Ds to switch off the player collisions and make them fall directly.
            if(shouldJump && !goDown){
				if(Math.Abs(GlobalPosition.Y - destination.Y) > 10) {
					Vector2 velocity = (destination - GlobalPosition) * 0.1f;
					MoveAndCollide(velocity);
				}
			}
			if(Math.Abs(GlobalPosition.Y - destination.Y) < 13){
				goDown = true;
				Vector2 velocity = (startPosition - GlobalPosition) * 0.1f;
				MoveAndCollide(velocity);
			}
        }

        public void OnArea2dBodyEntered(Node2D body){
			if(isSnappy) {
				shouldJump = true;
			}
		}

		public void OnTimerTimeout(){
			if(!isSnappy) changeSprite = true;
		}
	}
}