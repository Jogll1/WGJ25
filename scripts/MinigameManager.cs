using Godot;
using System;

namespace WGJ25
{
	public abstract partial class MinigameManager : Node2D
	{	
		protected private GameManager gameManager;
		
		// References
		private RichTextLabel popupText;
		private Timer stopwatchTimer;
		private AnimationPlayer stopwatchAnim;

		// Use this as a reference for objects in the scene 
		public bool GameEnded {get; private set;}

		public override void _Ready()
		{
			base._Ready();
			gameManager = GetNode<GameManager>("/root/GameManager");
			popupText = GetNode<RichTextLabel>("UI/UIParent/PopupTextParent/PopupText");
			stopwatchTimer = GetNode<Timer>("UI/UIParent/StopwatchParent/StopwatchTimer");
			stopwatchAnim = GetNode<AnimationPlayer>("UI/UIParent/StopwatchParent/StopwatchAnim");

			// Connect to the stopwatch timer's timeout signal
			Callable ca = new(this, nameof(OnStopwatchTimeout));
			stopwatchTimer.Connect("timeout", ca);

			// Connect to popup text animation player
			Callable cb = new(this, nameof(OnPopupTextAnimationFinished));
			GetNode<AnimationPlayer>("UI/UIParent/PopupTextParent/PopupTextAnim").Connect("animation_finished", cb);
		}

		private void OnPopupTextAnimationFinished(string animationName) 
		{
			// Called when the popup text animation finishes
			if (animationName == "popup_text") 
			{
				StartStopwatch();
			}
		}

		//Use to prematurely end the game if some failstate has been reached
		protected void EndGame(){
			stopwatchTimer.Stop();
			GameEnded = true;
			gameManager.LoadNextGame();
			GD.Print("Game finished!");
		}

		protected void StartStopwatch() 
		{
			// Call this to start the game's 8 second timer
			stopwatchTimer.Start();

			// Play the stopwatch animation
			stopwatchAnim.Play("count");
		}

		protected virtual void OnStopwatchTimeout() 
		{
			// Called when the game's timer runs out
			stopwatchTimer.Stop();
			GameEnded = true;
			GD.Print("Game finished!");
		}
		
		protected void SetPopupText(string text) 
		{
			// Set the popup message text - this should be called in the _Ready() method of a MinigameManager subclass
			popupText.Text = $"[center]{text}";
		}

		public double GetTimerTimeLeft() 
		{
			return stopwatchTimer.TimeLeft;
		}
	}
}