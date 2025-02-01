using Godot;
using System;

namespace WGJ25
{
	public partial class Spear : RigidBody2D
	{
		private int moveSpeed = 15;
        private float angle = Mathf.Pi / 4;
        private float xDir;
        private Godot.Vector2 velocity = new();
        private float initY;
        private int counter = 0;

        public override void _Ready()
        {
            initY = GlobalPosition.Y;
        }

        public override void _PhysicsProcess(double delta)
        {
            // Arch motion
            if(GlobalPosition.Y <= initY)
            {
                // counter++;
                // velocity = new Godot.Vector2(
                //     speed * Mathf.Cos(angle) * xDir,
                //     -(speed * Mathf.Sin(angle) - 9.81f * counter / 25)
                // );
                // SetSpearRotation();

                if (GlobalPosition.X >= GameManager.SCREEN_WIDTH || GlobalPosition.X <= 0) 
				{
					Disable();
				}

				if (GlobalPosition.Y >= GameManager.SCREEN_HEIGHT || GlobalPosition.Y <= 0) 
				{
					Disable();
				}
                
                GlobalPosition = new Godot.Vector2(
					Mathf.Clamp(GlobalPosition.X, 0, GameManager.SCREEN_WIDTH),
					Mathf.Clamp(GlobalPosition.Y, 0, GameManager.SCREEN_HEIGHT)
				);

                MoveAndCollide(velocity);
            }
        }

        public void SetVelocity(Godot.Vector2 dir)
        {
            counter = 0;
			// xDir = dir.X;
            velocity = dir * moveSpeed;
            // SetRotation(Mathf.Pi / 4 * -xDir);
            SetSpearRotation();
        }

        private void SetSpearRotation() 
        {
            SetRotation(Mathf.Atan2(velocity.Y, velocity.X));
        }

        public void OnBodyEntered(Node2D body) 
        {
            if(body.IsInGroup("Dodo"))
            {
                body.QueueFree();
                ((HuntingGameManager)GetParent()).DodosKilled++;
                Disable();   
            }
        }

        private void Disable()
        {
            SetDeferred("freeze", true);
            SetDeferred("lock_rotation", true);
            velocity = Godot.Vector2.Zero;
            LinearVelocity = Godot.Vector2.Zero;
        }
    }
}