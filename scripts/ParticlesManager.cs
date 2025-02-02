using Godot;

namespace WGJ25
{
	public static class ParticlesManager
	{
		public static GpuParticles2D SpawnParticles(string scenePath, Vector2 pos, Node parent)
		{
			PackedScene sceneInstance = (PackedScene)GD.Load(scenePath);
			GpuParticles2D particles = (GpuParticles2D)sceneInstance.Instantiate();
			parent.AddChild(particles);
			particles.Position = pos;
			particles.Emitting = true;

			return particles;
		}
	}
}