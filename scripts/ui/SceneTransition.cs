using Godot;
using System;

namespace WGJ25
{
	public partial class SceneTransition : CanvasLayer
	{
		[Signal] public delegate void SceneChangedEventHandler();

		// Change the scene and play a transition
		public async void ChangeScene(string target)
		{
			// AnimationPlayer anim = GetNode<AnimationPlayer>("AnimationPlayer");
			// anim.Play("dissolve");
			// await ToSignal(anim, "animation_finished");
			// GetTree().ChangeSceneToFile(target);
			// anim.PlayBackwards("dissolve");

			// EmitSignal(SignalName.SceneChanged);
		}
	}
}
