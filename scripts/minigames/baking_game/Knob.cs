using Godot;
using System;

namespace WGJ25
{
	public partial class Knob : TextureButton
	{
		private const float fifteenDegrees = Mathf.Pi / 6;

		public override void _Ready()
		{
			
		}

		public override void _Process(double delta)
		{
			
		}

		public void OnKnobDown()
		{
			SetRotation(Rotation + fifteenDegrees);
		}
	}
}
