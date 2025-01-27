using Godot;
using System;

namespace WGJ25
{
	public partial class Spear : RigidBody2D
	{
		private int speed = 10;
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
            // GlobalPosition += velocity;

            // Arch motion
            if(GlobalPosition.Y <= initY)
            {
                counter++;
                velocity = new Godot.Vector2(
                    speed * Mathf.Cos(angle) * xDir,
                    -(speed * Mathf.Sin(angle) - 9.81f * counter / 50)
                );
                SetSpearRotation();
                MoveAndCollide(velocity);
            }
        }

        public void SetVelocity(Godot.Vector2 dir)
        {
            counter = 0;
			xDir = dir.X;
        }

        public void SetSpearRotation() 
        {
            SetRotation(Mathf.Atan2(velocity.Y, velocity.X));
        }
    }
}