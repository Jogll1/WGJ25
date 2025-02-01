using Godot;
using System;
using System.Runtime.Serialization;

namespace WGJ25{
	public partial class Ingredient : RigidBody2D
	{
		public int health = 2;
		private MixingGameManager manager;
		private const string CRUSHED_INGREDIENT_SCENE_PATH = "res://scenes/minigames/mixing_game/CrushedIngredient.tscn";
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			manager = (MixingGameManager)GetParent();
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			if(health <= 0){
				GD.Print("Crushed");
				ObjectManager.SpawnObject(CRUSHED_INGREDIENT_SCENE_PATH, this.GlobalPosition, manager);
				Free();
			}
		}

		public void OnArea2dAreaEntered(Area2D area){
			if(area.IsInGroup("Ladle")){
				GD.Print("Getting Crushed");
				health -= 1;
				ApplyImpulse(new Vector2(0, -100));
			}
		}
	}

}