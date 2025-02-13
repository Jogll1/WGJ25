using Godot;
using System;

namespace WGJ25{
    //TODO: Add a death "animation" that just makes the player fall down once hit by a crocodile
	public partial class SnappyPlayer : RigidBody2D
	{
        public bool IsDead {get{return dead;} set {dead = value;}}
        public bool caught = false;
        public CollisionShape2D collider;
        private Timer timer;
        private bool isColliding = true;
        private bool dead = false;
        public override void _Ready()
        {
            collider = GetNode<CollisionShape2D>("CollisionShape2D");
            timer = GetNode<Timer>("Timer");
            ProcessMode = Node.ProcessModeEnum.Pausable;
        }

        public override void _Input(InputEvent @event)
        {
            if(Input.IsActionJustPressed("move_up")&&isColliding&&!dead) {
                ApplyImpulse(new Vector2(170, -400));
                isColliding = false;
			}
        }

        public void OnArea2dBodyEntered(Node2D body){
            isColliding = true;
        }

        public void HurtboxEntered(Area2D area){
            GD.Print("Died");
            if(!caught) Sleeping = true;
            caught = true;
        }
    }
}