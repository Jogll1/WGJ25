using Godot;
using System;

namespace WGJ25
{
	public partial class MinigameManager : Node2D
	{	
		protected private GameManager gameManager;
		
		// References
		private CanvasLayer popupText;

		public override void _Ready()
		{
			gameManager = GetNode<GameManager>("/root/GameManager");
			popupText = GetNode<CanvasLayer>("PopupText");
		}
		
		protected void SetPopupText(String text) 
		{
			// Set the popup message text - this should be called in the _Ready() method of a MinigameManager subclass
			popupText.GetNode<RichTextLabel>("UIParent/Text").Text = $"[center]{text}";
		}
	}
}