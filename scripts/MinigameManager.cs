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

		public override void _Ready()
		{
			gameManager = GetNode<GameManager>("/root/GameManager");
			popupText = GetNode<RichTextLabel>("UI/UIParent/PopupTextParent/PopupText");
			stopwatchTimer = GetNode<Timer>("UI/UIParent/StopwatchParent/StopwatchTimer");

			// Connect to the stopwatch timer's timeout signal
			Callable callable = new(this, nameof(OnStopwatchTimeout));
			stopwatchTimer.Connect("timeout", callable);
		}

		protected void StartStopwatch() 
		{
			// Call this to start the game's 8 second timer
			stopwatchTimer.Start();
		}

		protected virtual void OnStopwatchTimeout() 
		{
			// Called when the game's 8 second timer runs out
			GD.Print("Game finished!");
		}
		
		protected void SetPopupText(String text) 
		{
			// Set the popup message text - this should be called in the _Ready() method of a MinigameManager subclass
			popupText.Text = $"[center]{text}";
		}
	}
}