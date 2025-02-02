using Godot;
using System;

namespace WGJ25
{
	public partial class Hunter : CharacterBody2D
	{
		private const string SPEAR_PATH = "res://scenes/minigames/hunting_game/spear.tscn";
		private const string BONK_PARTICLES_PATH = "res://scenes/particles/bonk.tscn";

		private HuntingGameManager huntingGameManager;
		private Timer timer;
		private Area2D clubbingArea;
		private AnimationPlayer anim;

		private int moveSpeed = 600;
		private bool canFire = true;
		private int halfSize = 24;
		private float dirX = 1;
		private bool isClubbing = false;
		private bool hit = false;

        public override void _Ready()
        {
            huntingGameManager = (HuntingGameManager)GetParent();
			anim = GetNode<AnimationPlayer>("AnimationPlayer");
			timer = GetNode<Timer>("Timer");
        }

        public override void _Process(double delta)
        {
			if (!huntingGameManager.GameEnded && Input.IsActionPressed("fire") && canFire) 
			{
				canFire = false;
				timer.Start();

				// Spawn spear
				// Spear spear = (Spear)ObjectManager.SpawnObject(SPEAR_PATH, GlobalPosition, GetParent());
				// spear.SetVelocity(lastDir);

				// Club
				isClubbing = true;
				anim.Play("Swing");

				hit = false;
			}
        }

        public override void _PhysicsProcess(double delta)
        {
			if (!huntingGameManager.GameEnded) 
			{
				Vector2 dir = Input.GetVector("move_left", "move_right", "move_up", "move_down");
				Velocity = dir.Normalized() * moveSpeed;

				// Clamp pos
				GlobalPosition = new Godot.Vector2(
					Mathf.Clamp(GlobalPosition.X, halfSize, GameManager.SCREEN_WIDTH - halfSize),
					Mathf.Clamp(GlobalPosition.Y, halfSize, GameManager.SCREEN_HEIGHT - halfSize)
				);

				MoveAndSlide();
				SetAnim();
			}
        }

		public void OnBodyEntered(Node2D body)
		{
			if (body.IsInGroup("Dodo") && isClubbing && !hit)
			{
				ParticlesManager.SpawnParticles(BONK_PARTICLES_PATH, body.GlobalPosition, GetParent());
				hit = true;
				huntingGameManager.DodosKilled++;
				((Dodo)body).Kill();
			}
		}

		public void SetAnim()
		{
			if (Input.IsActionJustPressed("move_left")) 
			{
				Scale = new (1, -1);
				Rotation = Mathf.Pi;
			}
			if (Input.IsActionJustPressed("move_right")) 
			{
				Scale = new (1, 1);
				Rotation = 0;
			}
		}

		public void OnAnimFinished(string animName)
		{
			if(animName == "Swing")
			{
				isClubbing = false;
			}
		}

		public void OnTimerTimeout() 
		{
			canFire = true;
		}
    }
}