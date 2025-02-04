using Godot;
using System;

namespace WGJ25
{
	public abstract partial class MinigameManager : Node2D
	{	
		protected private GameManager gameManager;
		
		// References
		private RichTextLabel popupText;
		private RichTextLabel controlText;
		private Timer stopwatchTimer;
		private Timer instructionTimer;
		private AnimationPlayer stopwatchAnim;
		private Sprite2D instructions;
		private bool displayControls = false;

		// Use this as a reference for objects in the scene 
		public bool GameEnded {get; private set;}

		public override void _Ready()
		{
			gameManager = GetNode<GameManager>("/root/GameManager");
			popupText = GetNode<RichTextLabel>("UI/UIParent/PopupTextParent/PopupText");
			controlText = GetNode<RichTextLabel>("UI/UIParent/ControlTextParent/ControlText");
			stopwatchTimer = GetNode<Timer>("UI/UIParent/StopwatchParent/StopwatchTimer");
			stopwatchAnim = GetNode<AnimationPlayer>("UI/UIParent/StopwatchParent/StopwatchAnim");
			instructionTimer = GetNode<Timer>("InstructionTimer");
			instructions = GetNode<Sprite2D>("Instructions");

			// Connect to the stopwatch timer's timeout signal
			Callable ca = new(this, nameof(OnStopwatchTimeout));
			stopwatchTimer.Connect("timeout", ca);

			// Connect to popup text animation player
			Callable cb = new(this, nameof(OnPopupTextAnimationFinished));
			GetNode<AnimationPlayer>("UI/UIParent/PopupTextParent/PopupTextAnim").Connect("animation_finished", cb);

			Callable ci = new(this, nameof(OnInstructionTimerTimeout));
			instructionTimer.Connect("timeout", ci);

			controlText.Hide();
			ProcessMode = Node.ProcessModeEnum.Always;
		}

        public override void _Process(double delta)
        {
            if(Input.IsActionJustPressed("controls")){
				if(!displayControls){
					GetTree().Paused = true;
					GD.Print("Displaying Controls");
					displayControls = true;
					controlText.Visible = true;
					PauseStopwatch();
				}
				else {
					GetTree().Paused = false;
					GD.Print("Hiding Controls");
					displayControls = false;
					controlText.Visible = false;
					UnpauseStopwatch();
				}
			}
        }

        private void OnPopupTextAnimationFinished(string animationName) 
		{
			// Called when the popup text animation finishes
			if (animationName == "popup_text") 
			{
				StartStopwatch();
			}
			else if(animationName == "popup_text_quick")
			{
				// load final scene
				GD.Print("Done.");
				gameManager.LoadEnding();
			}
		}

		//Use to prematurely end the game if some failstate has been reached
		protected void EndGame()
		{
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

		protected void PauseStopwatch(){
			stopwatchTimer.Paused = true;
			stopwatchAnim.Pause();
		}

		protected void UnpauseStopwatch(){
			stopwatchTimer.Paused = false;
			stopwatchAnim.Play("count");
		}

		protected virtual void OnStopwatchTimeout() 
		{
			// Called when the game's timer runs out
			stopwatchTimer.Stop();
			GameEnded = true;
			GD.Print("Game finished!");
		}

		protected void OnInstructionTimerTimeout(){
			GD.Print("Instructions Hidden");
			instructions.Hide();
		}
		
		protected void SetPopupText(string text) 
		{
			// Set the popup message text - this should be called in the _Ready() method of a MinigameManager subclass
			popupText.Text = $"[center]{text}";
		}

		protected void SetControlText(string text){
			controlText.Text = $"[center]{text}";
		}

		public double GetTimerTimeLeft() 
		{
			return stopwatchTimer.TimeLeft;
		}

		public void EndWholeGame()
		{
			popupText.Text =  $"[center]Finished!";
			GetNode<AnimationPlayer>("UI/UIParent/PopupTextParent/PopupTextAnim").Play("popup_text_quick");
		}
	}
}
