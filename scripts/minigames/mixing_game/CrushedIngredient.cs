using Godot;
using System;

namespace WGJ25{
	public partial class CrushedIngredient : RigidBody2D
	{
		// Called when the node enters the scene tree for the first time.
		public int index = 0;
		private Sprite2D sprite;
		private string[] crushedIngredientTextures = {"res://art/minigames/mixing/Crushed Cabbage.png", 
												"res://art/minigames/mixing/Crushed dodo.png", 
												"res://art/minigames/mixing/Crushed egg.png",
												"res://art/minigames/mixing/Crushed milk.png",
												"res://art/minigames/mixing/Crushed Wheatbag.png"};
		public override void _Ready()
		{
			sprite = GetNode<Sprite2D>("Sprite2D");
			sprite.Texture = GD.Load<Texture2D>(crushedIngredientTextures[index]);
			ProcessMode = Node.ProcessModeEnum.Pausable;
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _PhysicsProcess(double delta)
		{
			//MoveAndCollide(new Vector2(0, -10));
		}
	}
}