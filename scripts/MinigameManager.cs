using Godot;
using System;

namespace WGJ25
{
	public partial class MinigameManager : Node2D
	{	
		protected GameManager gameManager;
		
		// References
		private CanvasLayer popupText;
		
		// This will be displayed at the start of the game as a popup message
		protected String popupMessage = "GAME_INSTRUCTION";

		public override void _Ready()
		{
			gameManager = GetNode<GameManager>("/root/GameManager");
			popupText = GetNode<CanvasLayer>("PopupText");

			// Set the popup message text
			popupText.GetChildren()[0].GetNode<RichTextLabel>("Text").Text = $"[center]{popupMessage}";
		}
	}
}