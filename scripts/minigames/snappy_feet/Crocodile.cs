using Godot;
using System;

namespace WGJ25{
	public partial class Crocodile : RigidBody2D
	{
		public bool IsSnappy {get {return isSnappy;} set {isSnappy = value;}}
		private bool isSnappy;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{

		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			if(isSnappy){
				Modulate = new Color(1, 0, 0);
			}
			else {
				Modulate =  new Color(0, 1, 0);
			}
		}
	}
}