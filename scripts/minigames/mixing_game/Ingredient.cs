using Godot;
using System;
using System.Runtime.Serialization;

namespace WGJ25{
	public partial class Ingredient : RigidBody2D
	{
		public int health = 2;
		public Sprite2D sprite;
		public int index = 0;
		private MixingGameManager manager;
		private const string CRUSHED_INGREDIENT_SCENE_PATH = "res://scenes/minigames/mixing_game/CrushedIngredient.tscn";

		private string[] ingredientTextures = {"res://art/minigames/mixing/Cabbage.png", 
												"res://art/minigames/mixing/DodoDrumstick.png", 
												"res://art/minigames/mixing/Egg.png",
												"res://art/minigames/mixing/Milk.png",
												"res://art/minigames/mixing/wheatbag.png"};

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			sprite = GetNode<Sprite2D>("Sprite2D");
			this.sprite.Texture = GD.Load<Texture2D>(ingredientTextures[index]);
			manager = (MixingGameManager)GetParent();
			ProcessMode = Node.ProcessModeEnum.Pausable;
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			if(health <= 0){
				GD.Print("Crushed");
				CrushedIngredient temp = (CrushedIngredient)ObjectManager.SpawnObject(CRUSHED_INGREDIENT_SCENE_PATH, this.GlobalPosition, manager);
				temp.index = this.index;
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